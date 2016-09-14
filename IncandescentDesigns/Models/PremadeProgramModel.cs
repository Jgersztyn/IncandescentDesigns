using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class PremadeProgramModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        //image goes here
        [NotMapped]
        [Display(Name = "Image")]
        public HttpPostedFileBase Attachment { get; set; }
        public string Image { get; set; }

        //.ino file goes here
        [NotMapped]
        [Display(Name = "INO File")]
        public HttpPostedFileBase FileToUpload { get; set; }
        public byte[] File { get; set; }

        //name of .ino file
        //[Display(Name = "Arduino Code")
        public string FileName { get; set; }
        
    }
}