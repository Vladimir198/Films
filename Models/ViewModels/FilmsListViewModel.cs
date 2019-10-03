using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models.ViewModels
{
    public class FilmsListViewModel
    {
        public FilmsListViewModel()
        {
            Films = new List<FilmViewModel>();
        }
        public List<FilmViewModel> Films { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}