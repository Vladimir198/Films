using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название фильма")]
        public string Name { get; set; }

        [Required]
        [StringLength(800, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 4)]
        [Display(Name = "Описание фильма")]
        public string Description { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Режесер фильма")]
        public string Producer { get; set; }

        [Required]
        [Display(Name = "Год создания фильма")]
        public int CreateYear { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string PosterUrl
        {
            get
            {
                return $"Home/Poster/{Id}";
            }
        }

    }
}