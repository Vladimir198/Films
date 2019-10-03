using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsCatalog.Models.DataModels;
using FilmsCatalog.Models.ViewModels;

namespace FilmsCatalog.Models.Mapping
{
    public class FilmModelMapper : MapperConfiguration
    {
        public FilmModelMapper() : base(cnfg => {
            cnfg.CreateMap<FilmDataModel, FilmViewModel>()
            .ForMember(f => f.UserName, e => e.MapFrom(f => f.User.UserName));

            cnfg.CreateMap<FilmViewModel, FilmDataModel>()
            .ForMember(f => f.User, e => e.Ignore());
        })
        {}
    }
}