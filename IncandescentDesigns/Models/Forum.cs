using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace IncandescentDesigns.Models
{
    public class Forum
    {
        [Key]
        public int ForumId { get; set; }
        public string ForumTitle { get; set; }
        public virtual ICollection<ForumTopic> Threads { get; set; }
        public string Description { get; set; }

    }

}