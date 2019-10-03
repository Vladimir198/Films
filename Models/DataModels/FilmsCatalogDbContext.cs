using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FilmsCatalog.Models.DataModels
{
    public class FilmsCatalogDbContext : IdentityDbContext<ApplicationUser>
    {
        public FilmsCatalogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FilmsCatalogDbContext Create()
        {
            return new FilmsCatalogDbContext();
        }

        public DbSet<FilmDataModel> Films { get; set; }

    }
}