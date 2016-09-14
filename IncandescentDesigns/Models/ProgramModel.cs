using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncandescentDesigns.Models
{
    public class ProgramModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Mood { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string programLocation { get; set; }
    }
}