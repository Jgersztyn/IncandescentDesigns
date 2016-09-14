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
using IncandescentDesigns.Handlers;

namespace IncandescentDesigns.Controllers
{
    public class NewsFeedController : Controller
    {
        private IncandescentDesignsContext db = new IncandescentDesignsContext();

        // GET: NewsFeed
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var stories = from s in db.News
                           select s;
            switch (sortOrder)
            {
                case "title_desc":
                    stories = stories.OrderByDescending(s => s.Title);
                    break;
                case "Author":
                    stories = stories.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    stories = stories.OrderByDescending(s => s.Author);
                    break;
                case "Date":
                    stories = stories.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    stories = stories.OrderByDescending(s => s.PostDate);
                    break;
                default:
                    stories = stories.OrderBy(s => s.Title);
                    break;
            }

            return View(stories.ToList());
        }

        //GET NewsStory
        public ActionResult Story(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.News.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // GET: NewsFeed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.News.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // GET: NewsFeed/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: NewsFeed/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,Body,Attachment")] NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {

                var file = newsFeed.Attachment;
                if (file != null)
                {
                    string fileExtension = file.FileName;
                    fileExtension = fileExtension.Substring(fileExtension.Length - 4).ToLower();
                    if (fileExtension == "jpeg" || fileExtension == ".jpg" || fileExtension == ".png")
                    {
                        //set up the image for upload
                        ImageHandler bh = new ImageHandler("images");
                        string tmp = bh.Upload(file);
                        newsFeed.Image = tmp;
                    }
                    //exention type is not valid
                    else
                    {
                        ModelState.AddModelError("", "Image may only be PNG or JPG.");
                        return View("Create", newsFeed);
                    }
                }

                newsFeed.PostDate = DateTime.Now;
                newsFeed.Author = User.Identity.Name;
                db.News.Add(newsFeed);
                db.SaveChanges();
                return RedirectToAction("Story", "NewsFeed", new { id = newsFeed.PostID });
            }

            return View(newsFeed);
        }

        // GET: NewsFeed/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.News.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // POST: NewsFeed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Title,Author,Body,PostDate,Attachment")] NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {

                var file = newsFeed.Attachment;
                if (file != null)
                {
                    ImageHandler bh = new ImageHandler("images");
                    string tmp = bh.Upload(file);
                    newsFeed.Image = tmp;
                }
                db.Entry(newsFeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsFeed);
        }

        // GET: NewsFeed/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsFeed newsFeed = db.News.Find(id);
            if (newsFeed == null)
            {
                return HttpNotFound();
            }
            return View(newsFeed);
        }

        // POST: NewsFeed/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsFeed newsFeed = db.News.Find(id);
            db.News.Remove(newsFeed);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Sorting for NewsFeed/Search
        public ActionResult Search(string searchString)
        {
            var news = from s in db.News
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Title.Contains(searchString)
                                       || s.Body.Contains(searchString));
                return View(news.ToList());
            }
            return View();
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
