using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncandescentDesigns.Models
{
    public class NewsFeed
    {
        [Key]
        public int PostID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string Author { get; set; }
        [Required]
        public DateTime PostDate { get; set; }

        //public string Picture { get; set; }
        [NotMapped]
        [Display(Name = "Header Image")]
        public HttpPostedFileBase Attachment { get; set; }
        public string Image { get; set; }

        //[Display(Name = "image display")]
        //public bool DisplayItem { get; set; }
    }
}