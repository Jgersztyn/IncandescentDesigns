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
using PagedList;
using System.IO;
using IncandescentDesigns.Handlers;
using Microsoft.AspNet.Identity;

namespace IncandescentDesigns.Controllers
{
    public class ProgramModelController : Controller
    {
        private IncandescentDesignsContext db = new IncandescentDesignsContext();

        // GET: ProgramModel
        public ActionResult Index(string sortOrder, string nameSearch, string moodSearch, string ownerSearch, string nameFilter, string moodFilter, string likesSearch, string ownerFilter, string likesFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MoodSortParm = String.IsNullOrEmpty(sortOrder) ? "mood_desc" : "";
            ViewBag.LikeSortParm = sortOrder == "Likes" ? "likes_desc" : "Likes";

            if(nameSearch != null || moodSearch != null || ownerSearch != null || likesSearch != null)
            {
                page = 1;
            }
            else
            {
                nameSearch = nameFilter;
                moodSearch = moodFilter;
                ownerSearch = ownerFilter;
                likesSearch = likesFilter;
            }

            ViewBag.NameFilter = nameSearch;
            ViewBag.MoodFilter = moodSearch;
            ViewBag.OwnerFilter = ownerSearch;
            ViewBag.LikesFilter = likesSearch;

            var programs = from p in db.Programs
                           select p;

            if (!String.IsNullOrEmpty(nameSearch))
            {
                programs = programs.Where(s => s.Name.Contains(nameSearch));
            }
            if (!String.IsNullOrEmpty(moodSearch))
            {
                programs = programs.Where(s => s.Mood.Contains(moodSearch));
            }
            if (!String.IsNullOrEmpty(ownerSearch))
            {
                programs = programs.Where(s => s.Owner.Contains(ownerSearch));
            }
            if (!String.IsNullOrEmpty(likesSearch))
            {
                int likes = 0;

                Int32.TryParse(likesSearch, out likes);
                programs = programs.Where(s => s.Likes >= likes);
            }


            switch (sortOrder)
            {
                case "mood_desc":
                    programs = programs.OrderByDescending(s => s.Mood);
                    break;
                case "Likes":
                    programs = programs.OrderBy(s => s.Likes);
                    break;
                case "likes_desc":
                    programs = programs.OrderByDescending(s => s.Likes);
                    break;
                default:
                    programs = programs.OrderBy(s => s.Mood);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(programs.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProgramModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramModel programModel = db.Programs.Find(id);
            if (programModel == null)
            {
                return HttpNotFound();
            }
            return View(programModel);
        }

        // GET: ProgramModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgramModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Owner,Mood")] ProgramModel programModel)
        {
            if (ModelState.IsValid)
            {
                programModel.CreatedOn = DateTime.Now;
                programModel.Likes = 1;
                db.Programs.Add(programModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programModel);
        }

        public ActionResult Like(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramModel programModel = db.Programs.Find(id);
            programModel.Likes++;
            if(programModel.Likes == 20)
            {
                string name = programModel.Name + ".c";
                string[] names = name.Split(' ');
                name = String.Concat(names);
                ProgramHandler ph = new ProgramHandler("programs");
                ph.Upload(name);
                programModel.programLocation = ph.getFile(name);
            }
            db.Entry(programModel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Favorite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteProgram favorite = new FavoriteProgram();
            favorite.StoredFileId = id.Value;
            favorite.UserId = User.Identity.GetUserId();
            db.FavoritePrograms.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ProgramModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramModel programModel = db.Programs.Find(id);
            if (programModel == null)
            {
                return HttpNotFound();
            }
            return View(programModel);
        }

        // POST: ProgramModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Owner,Mood,Likes,CreatedOn")] ProgramModel programModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programModel);
        }

        // GET: ProgramModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgramModel programModel = db.Programs.Find(id);
            if (programModel == null)
            {
                return HttpNotFound();
            }
            return View(programModel);
        }

        // POST: ProgramModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramModel programModel = db.Programs.Find(id);
            db.Programs.Remove(programModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
