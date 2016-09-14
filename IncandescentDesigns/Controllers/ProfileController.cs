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
using Microsoft.AspNet.Identity;
using IncandescentDesigns.Handlers;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace IncandescentDesigns.Controllers
{
    public class ProfileController : Controller
    {
        private IncandescentDesignsContext db = new IncandescentDesignsContext();

        // GET: Profile
        public ActionResult Index(string id)
        {
            List<Profile> profile = (from p in db.Profiles
                                     where p.UserId == id
                                     select p).ToList();
            if (!profile.Any())
            {
                return View();
            }
            return View(profile.First());
        }

        // GET: Profile/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,PhoneNumber,AboutMe,Interests")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!id.Equals(User.Identity.GetUserId()))
            {
                return View("Error");
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,PhoneNumber,AboutMe,Interests,ProfileVis,NameVis,PhoneNumVis,AboutVis,InterestsVis,PictureVis,FavoriteProgsVis,PictureLocation")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile", new { id = User.Identity.GetUserId() });
            }
            return View(profile);
        }

        public ActionResult UpdateProfilePic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfilePic(HttpPostedFileBase file)
        {

            //ensure the file type uploaded is either JPG or PNG
            string fileExtension = file.FileName;
            fileExtension = fileExtension.Substring(fileExtension.Length - 4).ToLower();

            if (fileExtension == "jpeg" || fileExtension == ".jpg" || fileExtension == ".png")
            {
                //is proper file type, so now update the image
                ImageHandler bh = new ImageHandler("images");
                string tmp = bh.Upload(file);
                string userId = User.Identity.GetUserId();

                var profile = (from p in db.Profiles
                               where p.UserId.Equals(userId)
                               select p).ToList().First();

                profile.PictureLocation = tmp;

                db.SaveChanges();

                return RedirectToAction("Index", "Profile", new { id = User.Identity.GetUserId() });
            }
            else
            {
                //not PNG or JPG

                //display a pop up asking user to upload the right file type
                return View("UpdateProfilePic");
            }
        }

        // GET: Profile/FavoritePrograms
        [Authorize]
        public ActionResult FavoritePrograms(string id)
        {
            IQueryable<FavoriteProgram> data = from p in db.FavoritePrograms
                                               where p.UserId.Equals(id)
                                               select p;

            return View(data.ToList());
        }

        /*
        * Manages the file download process of a previously uploaded program
        */
        [Authorize]
        public FileResult Download(int? id)
        {
            StoredFileModel fave = db.FavoritePrograms.Find(id).StoredFileModel;
            byte[] byteArray = fave.inoFile;

            return File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, fave.FileName);
        }

        public ActionResult SecuritySettings()
        {
            return View(db.Profiles.Find(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult SecuritySettings([Bind(Include = "UserId, Name, PhoneNumber, AboutMe, Interests,ProfileVis,NameVis,PhoneNumVis,AboutVis,InterestsVis,PictureVis,FavoriteProgsVis,PictureLocation")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile", User.Identity.GetUserId());
            }
            return View("Error");
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*
        * Links the favorites
        */
        //private Dictionary<string, string> SetupFavoritesMap(List<FavoriteProgram> programs)
        //{
        //    Dictionary<string, string> tmp = new Dictionary<string, string>();

        //    foreach (FavoriteProgram program in programs)
        //    {
        //        var progs = (from p in db.Programs
        //                     where p.Id == program.StoredFileId
        //                     select p).ToList().First();
        //        tmp.Add(progs.Name, progs.Mood);
        //    }

        //    return tmp;
        //}
        //private Dictionary<string, string> SetupFavoritesMap(List<FavoriteProgram> programs)
        //{
        //    Dictionary<string, string> tmp = new Dictionary<string, string>();

        //    foreach (FavoriteProgram program in programs)
        //    {
        //        var progs = (from p in db.Programs
        //                     where p.Id == program.StoredFileId
        //                    select p).ToList().First();
        //        tmp.Add(progs.Name, progs.Mood);
        //    }

        //    return tmp;
        //}

        public ActionResult EditProgram(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoredFileModel file = db.StoredFiles.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("EditProgram")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fileToUpdate = db.StoredFiles.Find(id);

            if (TryUpdateModel(fileToUpdate, "",
              new string[] { "Description" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("FavoritePrograms", "Profile", new { id = User.Identity.GetUserId() });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(fileToUpdate);
        }

        //public ActionResult ProgramDetails()
        public ActionResult ProgramDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoredFileModel file = db.StoredFiles.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        public ActionResult DeleteProgram(int? id)
        {
            StoredFileModel file = db.StoredFiles.Find(id);
            db.StoredFiles.Remove(file);
            db.SaveChanges();
            return RedirectToAction("FavoritePrograms", "Profile", new { id = User.Identity.GetUserId() });
        }

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
