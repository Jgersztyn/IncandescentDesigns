using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IncandescentDesigns.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        //last name not required
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string FromEmail { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }
}