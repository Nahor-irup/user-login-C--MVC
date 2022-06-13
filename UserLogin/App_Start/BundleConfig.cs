using System.Web;
using System.Web.Optimization;

namespace UserLogin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
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
