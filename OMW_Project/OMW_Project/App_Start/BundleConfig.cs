using System.Web.Optimization;

namespace OMW_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/js/bootstrap.min.js",
                      "~/Assets/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/site.css"));
            bundles.Add(new StyleBundle("~/LTE/Required/css").Include(
                "~/Assets/Vender/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/Assets/Vender/bower_components/font-awesome/css/font-awesome.min.css",
                "~/Assets/Vender/bower_components/Ionicons/css/ionicons.min.css",
                "~/Assets/Vender/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                "~/Assets/Vender/plugins/timepicker/bootstrap-timepicker.min.css",
                "~/Assets/Vender/bower_components/select2/dist/css/select2.min.css",
                "~/Assets/Vender/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css",
                "~/Assets/Vender/dist/css/AdminLTE.min.css",
                "~/Assets/Vender/dist/css/skins/_all-skins.min.css"));

            bundles.Add(new ScriptBundle("~/jquery/validate").Include(
                "~/Assets/Vender/plugins/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/LTE/Required/js").Include(
                "~/Assets/Vender/bower_components/jquery/dist/jquery.min.js",
                "~/Assets/Vender/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Assets/Vender/bower_components/datatables.net/js/jquery.dataTables.min.js",
                "~/Assets/Vender/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/Assets/Vender/bower_components/select2/dist/js/select2.full.min.js",
                "~/Assets/Vender/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/Assets/Vender/plugins/timepicker/bootstrap-timepicker.min.js",
                "~/Assets/Vender/dist/js/adminlte.min.js"));
        }
    }
}
