using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IncandescentDesigns.DAL;
using IncandescentDesigns.Models;
using System.IO;
using PagedList;
using IncandescentDesigns.Handlers;

namespace IncandescentDesigns.Controllers
{
    public class PremadeProgramController : Controller
    {
        private IncandescentDesignsContext db = new IncandescentDesignsContext();

        // GET: PremadeProgram
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.PremadePrograms
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    students = students.OrderByDescending(s => s.Title);
                    break;
                case "Description":
                    students = students.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    students = students.OrderByDescending(s => s.Description);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: PremadeProgram/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PremadeProgram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Attachment,FileToUpload")] PremadeProgramModel premadeProgramModel)
        {
            if (ModelState.IsValid)
            {

                //upload for the image
                var file = premadeProgramModel.Attachment;
                if (file != null)
                {

                    //ensure very large files are not uploaded
                    //if(file.ContentLength > 1000000)
                    //{
                    //    ModelState.AddModelError("", "File size must not be greater than 1 MB.");
                    //    return View("Create", premadeProgramModel);
                    //}

                    //get only the extension
                    string fileExtension = file.FileName;
                    fileExtension = fileExtension.Substring(fileExtension.Length - 4).ToLower();
                    //check if the extension matches a valid file type
                    if (fileExtension == "jpeg" || fileExtension == ".jpg" || fileExtension == ".png")
                    {
                        //set up the image for upload
                        ImageHandler bh = new ImageHandler("images");
                        string tmp = bh.Upload(file);
                        premadeProgramModel.Image = tmp;
                        //convert image into byte array for storage
                        //premadeProgramModel.Image = new byte[premadeProgramModel.Attachment.ContentLength];
                        //premadeProgramModel.Attachment.InputStream.Read(premadeProgramModel.Image, 0, premadeProgramModel.Image.Length);
                    }
                    //bad file type; ask user to try again with different file type
                    else
                    {
                        ModelState.AddModelError("", "Image may only be PNG or JPG.");
                        return View("Create", premadeProgramModel);
                    }
                }

                //upload for the .ino file
                var file2 = premadeProgramModel.FileToUpload;
                if (file2 != null)
                {
                    //convert file data into byte array for storage
                    premadeProgramModel.File = new byte[premadeProgramModel.FileToUpload.ContentLength];
                    premadeProgramModel.FileToUpload.InputStream.Read(premadeProgramModel.File, 0, premadeProgramModel.File.Length);

                    //save the name of the file
                    premadeProgramModel.FileName = ImageHandler.FormatName(file2.FileName);

                    //get only the extension
                    string fileExtension = file2.FileName;
                    fileExtension = fileExtension.Substring(fileExtension.Length - 4).ToLower();
                    //extension must be .ino only
                    if (fileExtension != ".ino")
                    {
                        ModelState.AddModelError("", "File may only be Arduino .ino");
                        return View("Create", premadeProgramModel);
                    }
                }
                //must upload a file when making a post
                else
                {
                    ModelState.AddModelError("", "Please select a .ino file to upload");
                    return View("Create", premadeProgramModel);
                }

                db.PremadePrograms.Add(premadeProgramModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(premadeProgramModel);
        }

        // GET: Edit
        [Authorize(Roles = "admin")]
        public ActionResult Edit()
        {
            return View();

            //accessibility control which allows everyone to enter the page
            //but only admins can do something
            //if
            //{
            //user.isInRole("admin");
            //} else { }
        }

        // GET: PremadeProgram/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PremadeProgramModel premade = db.PremadePrograms.Find(id);
            if (premade == null)
            {
                return HttpNotFound();
            }
            return View(premade);
        }

        // POST: PremadeProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PremadeProgramModel premade = db.PremadePrograms.Find(id);
            db.PremadePrograms.Remove(premade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Details about a file
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PremadeProgramModel premade = db.PremadePrograms.Find(id);
            if (premade == null)
            {
                return HttpNotFound();
            }
            return View(premade);
        }

        /*
        Manages the download of a file previously uploaded to the database
        @param id the specific id number of the file to be downloaded
        @returns the file to be downloaded
        */
        public FileResult Download(int? id)
        {
            PremadeProgramModel premade = db.PremadePrograms.Find(id);
            byte[] byteArray = premade.File;

            return File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, premade.FileName);
        }

        //Begin for NUnit tests

        public FileResult DownloadSomething(byte[] byteArray, string fileName)
        {
            return File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public string changeExtensionName(string nameOfFile, string newExtension)
        {
            nameOfFile = nameOfFile.Substring(nameOfFile.Length - 4);
            return nameOfFile + newExtension;
        }

        public FileResult changeFileExtension(FileResult file, string extension)
        {
            string fileName = file.FileDownloadName;
            file.FileDownloadName = changeExtensionName(fileName, extension);
            return file;
        }

        //End for NUnit tests

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
