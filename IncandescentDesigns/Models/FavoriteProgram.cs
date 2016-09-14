using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncandescentDesigns.Models
{
    public class FavoriteProgram
    {
        [Key]
        public int ID { get; set; }

        public int StoredFileId { get; set; }

        public string UserId { get; set; }

        public virtual StoredFileModel StoredFileModel { get; set; }

        public virtual Profile Profile { get; set; }
    }
}