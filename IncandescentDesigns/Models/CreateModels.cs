using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class CreateModels
    {
        [Key]
        public int PatternID { get; set; }
        [Display(Name = "Cube Size")]
        public string CubeSize { get; set; }
        public string Color { get; set; }
        public string Pattern { get; set; }
        //[RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed in the file name")]
        [RegularExpression(@"^[a-zA-Z0-9_\-]+$", ErrorMessage = "Only alphanumeric characters and underscores are allowed in the file name.")]
        [Required]
        public string FileName { get; set; }
        public string Sequence { get; set; }
        public string Speed { get; set; }
        public string Duration { get; set; }
        //specifically used to display the added patterns
        [Display(Name = "Added Patterns")]
        public string AddedPatterns { get; set; }
    }

}