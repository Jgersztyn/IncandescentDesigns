using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IncandescentDesigns.Models
{
    public class Profile
    {
        [Key]
        public string UserId { get; set; }
        public bool ProfileVis { get; set; }
        public string Name { get; set; }
        public bool NameVis { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumVis { get; set; }
        public string AboutMe { get; set; }
        public bool AboutVis { get; set; }
        public string Interests { get; set; }
        public bool InterestsVis { get; set; }
        public string PictureLocation { get; set; }
        public bool PictureVis { get; set; }
        public bool FavoriteProgsVis { get; set; }

        public virtual ICollection<FavoriteProgram> FavoritePrograms { get; set; }
    }
}