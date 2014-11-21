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
                "~/Content/jquery.simplecolorpicker.css",
                "~/Content/jquery.simple-dtpicker.css",
                "~/Content/jquery.datetimepicker.css",
                "~/Content/token-input.css",
                "~/Content/token-input-facebook.css",
                "~/Content/jquery-toastmessage/css/jquery.toastmessage.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}