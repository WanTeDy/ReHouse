using System.Web;
using System.Web.Optimization;

namespace ReHouse.FrontEnd
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery.1.10.min.js"
                        "~/Scripts/vendor/jquery.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/advertSettings").Include(
                        "~/Scripts/advertSettings.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/addtocart").Include(
                        "~/Scripts/addtocart.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                        "~/Content/fancybox/jquery.easing.1.3.js",
                        "~/Content/fancybox/jquery.fancybox-1.2.1.pack.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jcarouselLite").Include(
                        "~/Scripts/jcarousel_lite/jcarousellite_1.0.1c5.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jcarousel").Include(
                        "~/Scripts/jcarousel/js/jquery.jcarousel.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lazyLoad").Include(
                        "~/Scripts/lazyload.js"
                        //"~/Scripts/jquery.1.10.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/slides").Include(
                        "~/Scripts/slides.min.jquery.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/unob").Include(
                        "~/Scripts/vendor/jquery.unobtrusive-ajax.min.js"
                        , "~/Scripts/ajaxFunc.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/vendor/modernizr.js"
            //            ));


            bundles.Add(new StyleBundle("~/Content/styleCss").Include(
                     "~/Content/style.css",
                     "~/Content/reset.css",
                     "~/Content/minslider.css",
                     "~/Content/fancybox/jquery.fancybox.css"
                     ));
            //bundles.Add(new StyleBundle("~/Content/vendorCss").Include(
            //         "~/Content/vendor/*.css"));
            bundles.Add(new ScriptBundle("~/Scripts/indexJs").Include(
                   "~/Scripts/index.js"));
            //bundles.Add(new ScriptBundle("~/Scripts/coffeeJs").Include(
            //       "~/Scripts/coffee.js"));
            bundles.Add(new ScriptBundle("~/Scripts/modulesJs").Include(
                "~/Scripts/modules.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/vendorJs").Include(
                //"~/Scripts/vendor/jquery.cookie.min.js",
                //"~/Scripts/vendor/jquery.navgoco.min.js",
                //"~/Scripts/vendor/slick.min.js",
                //"~/Scripts/vendor/hoverintent.js",
                //"~/Scripts/vendor/superfish.js",
                //"~/Scripts/vendor/respond.min.js",
                "~/Scripts/vendor/jquery.validate.min.js",
                "~/Scripts/vendor/jquery.validate.unobtrusive.min.js",

                "~/Scripts/vendor/jquery.tooltipster.min.js"
                //"~/Scripts/vendor/jquery.formstyler.min.js",
                //"~/Scripts/vendor/jquery.tabslet.min.js",
                //"~/Scripts/vendor/moment.min.js",
                //"~/Scripts/vendor/ru.js",
                //"~/Scripts/vendor/rome.standalone.min.js",
                // "~/Scripts/vendor/jquery.magnific-popup.min.js"
                ));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                "~/Scripts/vendor/jquery.validate.min.js",
                "~/Scripts/vendor/jquery.validate.unobtrusive.min.js"));
            //            bundles.Add(new ScriptBundle("~/Scripts/client").Include(
            //         "~/Scripts/client/client.js"));
            //            bundles.Add(new ScriptBundle("~/Scripts/corp").Include(
            //  "~/Scripts/corp/corp.js"));
            bundles.Add(new ScriptBundle("~/Scripts/foundation").Include(
                "~/Scripts/fastclick.js",
                "~/Scripts/foundation.js"));
        }
    }
}
