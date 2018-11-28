using System.Web.Optimization;

namespace OnionExample
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/core").IncludeDirectory("~/Scripts/core", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/apps").IncludeDirectory("~/Scripts/apps", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
        }
    }
}
