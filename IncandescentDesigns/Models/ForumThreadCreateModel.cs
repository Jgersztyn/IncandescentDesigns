using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class ForumThreadCreateModel
    {
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
    }
}