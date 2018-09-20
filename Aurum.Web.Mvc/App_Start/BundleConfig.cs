using System.Web;
using System.Web.Optimization;
using System;

namespace Aurum.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Scripts

            // Scripts da página principal.
            bundles.Add(new ScriptBundle("~/bundles/Base/Principal").Include(
                        "~/Scripts/Base/Principal.js",
                        "~/Scripts/Base/Api.js"));

            // Scripts de jQuery e o plugin jQuery Templating.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Bibliotecas/jquery-1.*",
                        "~/Scripts/Bibliotecas/jQuery.tmpl.*",
                        "~/Scripts/Bibliotecas/jquery.ba-hashchange.*"));

            // Scripts da biblioteca jQuery UI.
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/Bibliotecas/jquery-ui*"));

            // Scripts da biblioteca jQuery Validation.
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Bibliotecas/jquery.unobtrusive*",
                        "~/Scripts/Bibliotecas/jquery.validate*"));

            // Scripts da biblioteca Modernizr.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Bibliotecas/modernizr-*"));
            
            #endregion Scripts


            #region Content/css

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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

            #endregion Content/css

        }
    }
}