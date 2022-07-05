using System.Web;
using System.Web.Optimization;

namespace UserLogin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            
            bundles.Add(new ScriptBundle("~/Custom/js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Assets/custom.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/j-query").Include(
                      "~/Assets/jquery-3.2.1.min.js"));

            bundles.Add(new StyleBundle("~/Custom/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Assets/custom.css"
                ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/font-awesome-5/css/fontawesome-all.min.css"
                ));


            bundles.Add(new StyleBundle("~/Adminlte/css").Include(
                      "~/Assets/adminlte.min.css"));
            bundles.Add(new ScriptBundle("~/Adminlte/js").Include(
                          "~/Assets/adminlte.min.js"));
        }
    }
}
