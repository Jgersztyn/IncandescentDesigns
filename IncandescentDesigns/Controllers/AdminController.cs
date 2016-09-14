using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncandescentDesigns.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IncandescentDesigns.DAL;

namespace IncandescentDesigns.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private IncandescentDesignsContext db = new IncandescentDesignsContext();
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(RegisterViewModel model)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
            };
            ir = um.Create(user, model.Password);
            ir = um.AddToRole(user.Id, "admin");
            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateForum()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForum([Bind(Include = "ForumId,ForumTitle,Description")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Forum.Add(forum);
                db.SaveChanges();
                return View("Index");
            }

            return View("Error");
        }
    }
}