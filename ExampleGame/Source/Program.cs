using System;
using Eto.Forms;
using Eto.Drawing;

namespace Game1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Application(Eto.Platform.Detect).Run(new MainForm());
        }
    }
}
