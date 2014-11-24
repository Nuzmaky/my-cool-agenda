using System.Web;
using System.Web.Optimization;

namespace CoolAgenda
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-dialog.js",
                        "~/Scripts/jquery.mask.js",
                        "~/Scripts/jquery.simplecolorpicker.js",
                        "~/Scripts/jquery.simple-dtpicker.js",
                        "~/Scripts/jquery.datetimepicker.js",
                        "~/Scripts/jquery.tokeninput.js",
                        "~/Scripts/jquery.qtip-1.0.0-rc3.js",
                        "~/Scripts/jquery.toastmessage.js",
                        "~/Scripts/utilidades-2.0.js",
                        "~/Scripts/jquery.tokeninput.js",
                        "~/Scripts/utilidades-2.0.js",
                        "~/Scripts/jquery.toastmessage.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));                                    

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-dialog.css",
                "~/Content/jquery.simplecolorpicker.css",
                "~/Content/jquery.simple-dtpicker.css",
                "~/Content/jquery.datetimepicker.css",
                "~/Content/token-input.css",
                "~/Content/token-input-facebook.css",
                "~/Content/jquery-toastmessage/css/jquery.toastmessage.css"));
           
            bundles.Add(new StyleBundle("~/Content/css/jqueryui/themes/start/css").Include(
                "~/Content/themes/base/jquery-ui-{version}.css"));
        }
    }
}