using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using IncandescentDesigns.DAL;
using IncandescentDesigns.Models;

namespace IncandescentDesigns.Handlers
{
    public class ImageHandler
    {
        private string imageDirecoryUrl;

        private CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public ImageHandler(string imageDirecoryUrl)
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

        public string GetProfilePic(string id)
        {
            return null;
        }

        public string Upload(HttpPostedFileBase file)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(imageDirecoryUrl);

            if (file != null)
            {
                string s = file.FileName;
                string z = FormatName(s);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(z);
                blockBlob.UploadFromStream(file.InputStream);
                return container.GetBlobReference(z).Uri.ToString();
            }

            return null;
        }

        public static string FormatName(string s)
        {
            if(s == null)
            {
                return null;
            }
            char[] delims = { '/', '\\' };
            string[] z = s.Split(delims);
            return z[z.Length-1];
        }
    }
}