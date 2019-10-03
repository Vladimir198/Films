using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using FilmsCatalog.Models.DataModels;
using FilmsCatalog.Models.ViewModels;
using FilmsCatalog.Models.Mapping;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace FilmsCatalog.Services
{
    public class FilmService
    {
        /// <summary>
        /// Возвращает список фильмов
        /// </summary>
        /// <param name="skip">сколько нужно пропустить, если 0 то ничего не пропускаем</param>
        /// <param name="take">сколько нужно получить, если 0 то берем все</param>
        /// <returns></returns>
        public async  Task<FilmsListViewModel> GetFilmsAsync(int skip, int take)
        {
            var result = new FilmsListViewModel();
            using (var ctx = new FilmsCatalogDbContext())
            {
                var q = ctx.Films
                   .OrderByDescending(f => f.Id)
                   .Include("User");
                result.TotalCount = q.Count();
                q = take == 0 ? q : q.Skip(skip).Take(take);
                result.Films = await q.ProjectToListAsync<FilmViewModel>(new FilmModelMapper());
            }
            return result;
        }

        public async Task<FilmViewModel> GetFilmByIdAsync(int id)
        {
            var film = new FilmViewModel();
            using (var ctx = new FilmsCatalogDbContext())
            {
                film = await ctx.Films
                    .Include("User")
                    .ProjectTo<FilmViewModel>(new FilmModelMapper())
                    .FirstOrDefaultAsync(f=>f.Id == id);
            }
            if (film == null)
            {
                throw new Exception($"Фильм с идентификатором {id} не найден в базе данных!");
            }
            return film;
        }

        public async Task<FilmViewModel> SaveFilmAsync(FilmViewModel film)
        {
            if (film == null)
            {
                throw new NullReferenceException($"Параметр film не должен быть null!");
            }
            var filmMapper = new FilmModelMapper().CreateMapper();
            var model = filmMapper.Map<FilmDataModel>(film);
            using (var ctx = new FilmsCatalogDbContext())
            {
                model = ctx.Films.Add(model);
                await ctx.SaveChangesAsync();
            }
            return filmMapper.Map<FilmViewModel>(model);
        }

        public async Task<FilmViewModel> EditFilmAsync(FilmViewModel film)
        {
            if (film == null)
            {
                throw new NullReferenceException($"Параметр film не должен быть null!");
            }
            FilmDataModel model;
            using (var ctx = new FilmsCatalogDbContext())
            {
                model = await ctx.Films
                    .Include("User")
                    .FirstOrDefaultAsync(f=>f.Id == film.Id);
                if (model == null)
                {
                    throw new Exception($"Фильм с идентификатором {film.Id} не найден в базе данных!");
                }
                model.Name = film.Name;
                model.Producer = film.Producer;
                model.Description = film.Description;
                model.CreateYear = film.CreateYear;
                model.MimeType = film.MimeType;
                model.FileName = film.FileName;
                await ctx.SaveChangesAsync();
            }
            var filmMapper = new FilmModelMapper().CreateMapper();
            return filmMapper.Map<FilmViewModel>(model);
        }

        public async Task<bool> RemoveFilmAsync(int id)
        {
            using (var ctx = new FilmsCatalogDbContext())
            {
                var model = await ctx.Films.FirstOrDefaultAsync(f=>f.Id == id);
                if (model == null)
                {
                    throw new Exception($"Фильм с идентификатором {id} не найден в базе данных!");
                }
                ctx.Films.Remove(model);
                await ctx.SaveChangesAsync();
            }
            return true;
        }
    } 
}