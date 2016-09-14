using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IncandescentDesigns.DAL;
using IncandescentDesigns.Models;

using Microsoft.AspNet.Identity;


namespace IncandescentDesigns.Controllers
{
    public class ForumController : Controller
    {

        private IncandescentDesignsContext db = new IncandescentDesignsContext();

        public ActionResult Index()
        {
            return View(db.Forum.ToList());
        }

        public ActionResult SubForum(int id)
        {
            Forum thread = db.Forum.Find(id);
            if(thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        public ActionResult Topic(int id)
        {
            ForumTopic topic = db.ForumThread.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        [Authorize]
        public ActionResult CreateTopic(int ForumId)
        {
            ForumThreadCreateModel tmp = new ForumThreadCreateModel();
            tmp.ForumId = ForumId;
            return View(tmp);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateTopic([Bind(Include = "ForumId,Title,Post")] ForumThreadCreateModel topic)
        {
            if(ModelState.IsValid)
            {
                ForumTopic tmp = new ForumTopic();

                tmp.UserId = User.Identity.GetUserId();
                tmp.CreatedOn = DateTime.Now;
                tmp.ForumId = topic.ForumId;
                tmp.Title = topic.Title;
                tmp.Post = topic.Post;

                //topic.UserId = User.Identity.GetUserId();
                //topic.CreatedOn = DateTime.Now;

                db.ForumThread.Add(tmp);
                db.SaveChanges();

                return RedirectToAction("Topic",new {id = tmp.ThreadId });
            }
            return View("Error");
        }

        [Authorize]
        public ActionResult AddReply(int id)
        {
            ForumPostCreateModel tmp = new ForumPostCreateModel();
            tmp.ThreadId = id;
            return View(tmp);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReply([Bind(Include = "ThreadId,Content")] ForumPostCreateModel post)
        {
            if(ModelState.IsValid)
            {
                ForumPost tmp = new ForumPost();

                tmp.UserId = User.Identity.GetUserId();
                tmp.Content = post.Content;
                tmp.Created = DateTime.Now;
                tmp.ThreadId = post.ThreadId;

                db.ForumPost.Add(tmp);
                db.SaveChanges();

                return RedirectToAction("Topic", new { id = tmp.ThreadId});
            }
            return View("Error");
        }
    }
}