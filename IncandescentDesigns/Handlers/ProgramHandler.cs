using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using IncandescentDesigns.Models;
using System.IO;

namespace IncandescentDesigns.Handlers
{
    public class ProgramHandler
    {
        private string imageDirecoryUrl;

        private CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public ProgramHandler(string imageDirecoryUrl)
        {
            this.imageDirecoryUrl = imageDirecoryUrl;
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference(imageDirecoryUrl);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            //Make available to everyone
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
        }

        public void Upload(string file)
        {
            FakeGenHandler tmp = new FakeGenHandler(file);
            MemoryStream stream = new MemoryStream();
            StreamWriter sw = new StreamWriter(stream);
            sw.Write(tmp.ToString());

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(imageDirecoryUrl);

            if (file != null)
            {
                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(file);
                        blockBlob.UploadFromStream(stream);
            }
        }

        public string getFile(string fName)
        {

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(imageDirecoryUrl);

            string blob = container.GetBlobReference(fName).Uri.ToString();

            return blob;
        }
    }
}