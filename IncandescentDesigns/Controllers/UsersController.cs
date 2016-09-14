using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using IncandescentDesigns.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace IncandescentDesigns.Controllers
{
    public class UsersController : Controller
    {

        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        // GET: Users
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed([DataType(DataType.EmailAddress)] string id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Search
        public ActionResult Search(string searchString)
        {
            if(User.IsInRole("admin") && String.IsNullOrEmpty(searchString))
            {
                return View(db.Users.ToList());
            }
            var admin = (from r in db.Roles
                        where r.Name.Equals("admin")
                        select r).ToList().First();
            var users = from u in db.Users
                        select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.Contains(searchString) || s.Email.Contains(searchString));
                var userList = users.ToList();
                if (!User.IsInRole("admin"))
                {
                    for (int i = userList.Count - 1; i >= 0; i--)
                    {
                        if (IsAdmin(userList[i]) || userList[i].EmailConfirmed == false)
                        {
                            userList.Remove(userList[i]);
                        }
                    }
                }
                return View(userList);
            }
            return View();
        }

        public bool IsAdmin(ApplicationUser user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user","User was null");
            }
            if (userManager.IsInRole(user.Id, "admin"))
            {
                return true;
            }
            return false;
        }

        // POST: Users/Search
/*        [HttpPost, ActionName("Delete")]
        public ActionResult Search(string name)
        {
            ViewBag.NameFilter = name;

            var users = from u in db.Users
                        select u;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(s => s.UserName.Contains(name) || s.Email.Contains(name));
            }
            return View(users.ToList());
        }*/
    }
}