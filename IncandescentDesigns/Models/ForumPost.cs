using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class ForumPost
    {
        [Key]
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public int ThreadId { get; set; }

        public virtual ForumTopic ForumTopic { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }
}