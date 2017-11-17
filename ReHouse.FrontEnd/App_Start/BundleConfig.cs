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

            bundles.Add(new ScriptBundle("~/bundles/imageLoader").Include(
                        "~/Scripts/imageLoader.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteImage").Include(
                        "~/Scripts/deleteImage.js"));

            bundles.Add(new ScriptBundle("~/bundles/deleteAdverts").Include(
                        "~/Scripts/deleteAdverts.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteManagers").Include(
                        "~/Scripts/deleteManagers.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteFeedbacks").Include(
                        "~/Scripts/deleteFeedbacks.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteVacancies").Include(
                        "~/Scripts/deleteVacancies.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteBuilders").Include(
                        "~/Scripts/deleteBuilders.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteFeedbacks").Include(
                        "~/Scripts/deleteFeedbacks.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteDistricts").Include(
                        "~/Scripts/deleteDistricts.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteBuildings").Include(
                        "~/Scripts/deleteBuildings.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/deleteArticles").Include(
                        "~/Scripts/deleteArticles.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/advertSettingsAdmin").Include(
                        "~/Scripts/advertSettingsAdmin.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/advertSettingsRentAdmin").Include(
                        "~/Scripts/advertSettingsRentAdmin.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/showDetails").Include(
                       "~/Scripts/detailsNews.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/advertSettingsRent").Include(
                        "~/Scripts/advertSettingsRent.js"
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
                        "~/Scripts/jquery.sliderPro.min.js"
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
                     "~/Content/fancybox/jquery.fancybox.css",
                     "~/Content/slider-pro.min.css",
                     "~/Content/mobile.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/styleCss2").Include(
                    "~/Content/animate.css",
                    "~/Content/owl.carousel.min.css",
                    "~/Content/owl.theme.default.min.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/stylecomm.css",
                    "~/Scripts/js-image-slider.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                    "~/Content/admin.css",
                    "~/Content/wysiwyg/jquery-te-1.4.0.css"
                    ));
            //bundles.Add(new StyleBundle("~/Content/vendorCss").Include(
            //         "~/Content/vendor/*.css"));
            bundles.Add(new ScriptBundle("~/Scripts/indexJs").Include(
                   "~/Scripts/index.js"));
            //bundles.Add(new ScriptBundle("~/Scripts/coffeeJs").Include(
            //       "~/Scripts/coffee.js"));
            bundles.Add(new ScriptBundle("~/Scripts/modulesJs").Include(
                "~/Scripts/modules.min.js",
                "~/Content/wysiwyg/jquery-te-1.4.0.min.js",
                "~/Scripts/mcVideoPlugin.js",
                "~/Scripts/js-image-slider.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/vendorJs").Include(
                //"~/Scripts/vendor/jquery.cookie.min.js",
                //"~/Scripts/vendor/jquery.navgoco.min.js",
                //"~/Scripts/vendor/slick.min.js",
                //"~/Scripts/vendor/hoverintent.js",
                //"~/Scripts/vendor/superfish.js",
                //"~/Scripts/vendor/respond.min.js",
                "~/Scripts/vendor/jquery.validate.min.js",
                "~/Scripts/vendor/jquery.validate.unobtrusive.min.js"

                //"~/Scripts/vendor/jquery.tooltipster.min.js",
                //"~/Scripts/vendor/jquery.formstyler.min.js"
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
