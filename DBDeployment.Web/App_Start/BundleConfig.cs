namespace DBDeployment.Web
{
    using System.Web.Optimization;

    /// <summary>
    /// BundleConfig class.
    /// </summary>
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundle(bundles);
            RegisterLessBundle(bundles);
            RegisterScriptBundle(bundles);
        }

        private static void RegisterStyleBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles/vendor")
                    .Include("~/Content/bootstrap.css"));
        }

        private static void RegisterLessBundle(BundleCollection bundles)
        {
            bundles.Add(new LessBundle("~/less/site")
                    .Include("~/Content/site.less"));
        }

        private static void RegisterScriptBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/vendor")
                    .Include(
                        "~/Scripts/jquery-2.1.3.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/bootstrap.js"));
        }
    }
}