using System.Web;
using System.Web.Optimization;

namespace FilmsCatalog
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.simplePagination.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                        "~/Scripts/fontawesome/fontawesome.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                       "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzone").Include(
                                "~/Content/dropzone/dropzone.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                     "~/Content/fontawesome.css"));

            
        }
    }
}
