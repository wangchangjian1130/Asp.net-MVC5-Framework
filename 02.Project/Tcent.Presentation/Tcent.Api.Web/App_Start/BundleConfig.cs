using System.Web.Optimization;

namespace Tcent.Api.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/tcentjs").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/modernizr-*",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/app.js",
                    "~/Scripts/select2.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/font-awesome.css",
                      "~/Content/AdminLTE.css",
                      "~/Content/skins.css",
                      "~/Content/css/select2.css",
                      "~/Content/site.css"));
        }
    }
}
