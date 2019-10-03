using FilmsCatalog.Models.ViewModels;
using FilmsCatalog.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private FilmService _filmService;

        public HomeController()
        {
            _filmService = new FilmService();
        }
        public ActionResult Index()
        {
            return View();
        }
         
        public async  Task<ActionResult> GetFilmsList(Pagination request)
        {
            try
            {
                var skip = request.PageSize * (request.CurrentPage - 1);
                var take = request.PageSize;
                var model = await _filmService.GetFilmsAsync(skip, take);
                model.CurrentPage = request.CurrentPage;
                model.PageSize = request.PageSize;
                return PartialView("FilmsListPartial", model);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }
        }

        [Authorize]
        public ActionResult AddFilm()
        {
            var model = new FilmViewModel();
            return View("AddFilmPartial", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFilm(FilmViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = User.Identity.GetUserId();
                    model = await _filmService.SaveFilmAsync(model);
                    if (model != null)
                    {
                        return RedirectToAction("FilmDetails", new { id = model.Id });
                    }
                }
                return View("AddFilmPartial", model);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> EditFilm(int id)
        {
            try
            {
                var model = await _filmService.GetFilmByIdAsync(id);
                return View(model);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFilm(FilmViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model = await _filmService.EditFilmAsync(model);
                    if (model != null)
                    {
                        return RedirectToAction("FilmDetails", new { id = model.Id });
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> FilmDetails(int id)
        {
            try
            {
                var model = await _filmService.GetFilmByIdAsync(id);
                return View("FilmDetailsPartial", model);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> RemoveFilm(int id)
        {
            try
            {
                await _filmService.RemoveFilmAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                return new HttpNotFoundResult(ex.Message);
            }


        }

        #region File

        [HttpPost]
        [Authorize]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                // получаем имя файла
                string fileName = $"poster_{file.FileName}";
                // сохраняем файл в папку Files в проекте
                var path = string.IsNullOrEmpty(Properties.Settings.Default.FileStoragePath)
                    ? Server.MapPath($"~/Posters/{fileName}")
                    : System.IO.Path.Combine(Properties.Settings.Default.FileStoragePath, fileName);
                file.SaveAs(path);
                var mimeType = file.ContentType;
                return Json(new { message =  "Ok", fileName , mimeType});
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        public async Task<FileResult> GetPosterFile(int id)
        {
            try
            {
                var film = await _filmService.GetFilmByIdAsync(id);
                
                var fileName = film.FileName;
                var mimeType = film.MimeType;
                var path = string.IsNullOrEmpty(Properties.Settings.Default.FileStoragePath)
                   ? Server.MapPath($"~/Posters/{fileName}")
                   : Path.Combine(Properties.Settings.Default.FileStoragePath, fileName);
                var body = GetFileBody(path);
                if (body.Length == 0)
                {
                    fileName = "poster.jpg";
                    mimeType = "jpg";
                    path = string.IsNullOrEmpty(Properties.Settings.Default.FileStoragePath)
                  ? Server.MapPath($"~/Posters/{fileName}")
                  : Path.Combine(Properties.Settings.Default.FileStoragePath, fileName);
                    body = GetFileBody(path);
                }
                return File(body, mimeType, fileName);
            }
            catch (Exception ex)
            {
                //TODO: Реализовать запись в лог.
                throw new HttpException(404, ex.Message);
            }
        }

        public byte[] GetFileBody(string filePath = "")
        {
            var result = new byte[0];
            if (System.IO.File.Exists(filePath))
            {
                result = System.IO.File.ReadAllBytes(filePath);
            }
            return result;
        }

        #endregion
    }
}