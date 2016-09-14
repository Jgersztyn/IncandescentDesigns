using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Models
{
    public class StoredFileModel
    {
        [Key]
        public int StoredFileId { get; set; }
        //specifically used for holding the created file
        public byte[] inoFile { get; set; }
        //used to recognize the file by name
        public string FileName { get; set; }
        //description for the file
        public string Description { get; set; }

        public virtual ICollection<FavoriteProgram> FavoritePrograms { get; set; }
    }
}