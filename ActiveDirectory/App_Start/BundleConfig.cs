using System.Web.Optimization;

namespace ActiveDirectory
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/View").Include(
                "~/assets/plugins/global/plugins.bundle.css",
                "~/assets/plugins/custom/prismjs/prismjs.bundle.css",
                "~/assets/css/style.bundle.css",
                "~/assets/css/themes/layout/header/base/light.css",
                "~/assets/css/themes/layout/header/menu/dark.css",
                "~/assets/css/themes/layout/brand/light.css",
                "~/assets/css/themes/layout/aside/dark.css"
            ));
            bundles.Add(new ScriptBundle("~/Content/Scripts").Include(
                "~/assets/plugins/global/plugins.bundle.js",
                "~/assets/plugins/custom/prismjs/prismjs.bundle.js",
                "~/assets/js/scripts.bundle.js",
                "~/Scriptsjquery-3.5.1.min.js"
            ));
        }
    }
}