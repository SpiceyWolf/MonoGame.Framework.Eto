namespace Eto.Forms
{
    /// <summary>
    /// Helper class to prevent code access outside the Visual Designer.
    /// May be used as well to stop the Designer after the Eto.Form
    /// InitializeComponent function to prevent errors while declaring
    /// other MonoGame content.
    /// </summary>
    public static class DesignMode
    {
        private static bool _active;
        private static bool _initialized;

        /// <summary>
        /// If running in VisualEditor, will return true.
        /// </summary>
        public static bool Active
        {
            get
            {
                if (!_initialized)
                {
                    var process = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    _active =
                        System.ComponentModel.LicenseManager.UsageMode ==
                        System.ComponentModel.LicenseUsageMode.Designtime ||
                        process == "devenv" || process == "MonoDevelop" || process == "XamarinStudio";
                    _initialized = true;
                }
                return _active;
            }
        }
    }
}