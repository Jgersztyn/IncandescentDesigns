using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class ForumTopic
    {
        [Key]
        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Post { get; set; }

        public DateTime CreatedOn { get; set; }
        //public int UserId { get; set; }


        //public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ForumPost> ForumPosts { get; set; }
        public virtual Forum Forum { get; set; }
    }
}