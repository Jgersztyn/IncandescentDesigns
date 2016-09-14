using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IncandescentDesigns.Models;

namespace IncandescentDesigns.DAL
{
    public class IncandescentDesignsInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<IncandescentDesignsContext>
    {
        protected override void Seed(IncandescentDesigns.DAL.IncandescentDesignsContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Forum gd = new Forum();
            gd.ForumTitle = "General Discussion";
            gd.ForumId = 1;
            gd.Description = "Forum for general discussion related to LED cubes and our site.";
            context.Forum.Add(gd);

            Forum help = new Forum();
            help.ForumTitle = "Help";
            help.ForumId = 2;
            help.Description = "Post all help requests here.";
            context.Forum.Add(help);

            Forum suggestions = new Forum();
            suggestions.ForumTitle = "Suggestions";
            suggestions.ForumId = 3;
            suggestions.Description = "Post all website suggestions here.";
            context.Forum.Add(suggestions);

            Forum ot = new Forum();
            ot.ForumTitle = "Off-Topic";
            ot.ForumId = 4;
            ot.Description = "All other discussions belong here.";
            context.Forum.Add(ot);

            ForumTopic thread = new ForumTopic();
            thread.ThreadId = 1;
            thread.ForumId = 1;
            thread.Title = "Announcements";
            thread.CreatedOn = DateTime.Now;
            context.ForumThread.Add(thread);

            ForumPost post = new ForumPost();
            post.PostId = 1;
            post.ThreadId = 1;
            post.Created = DateTime.Now;
            post.Content = "Test";
            context.ForumPost.Add(post);

            context.SaveChanges();
        }
    }
}
