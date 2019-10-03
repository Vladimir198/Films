using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models.DataModels
{
    [Table(name:"Films")]
    public class FilmDataModel
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(800)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Producer { get; set; }
        public int CreateYear { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}