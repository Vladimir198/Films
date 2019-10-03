using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmsCatalog.Models.ViewModels
{
    public class Pagination
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}