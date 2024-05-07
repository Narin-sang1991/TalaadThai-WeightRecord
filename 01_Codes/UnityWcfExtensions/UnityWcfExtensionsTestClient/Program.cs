using System;
using System.ServiceModel;
using System.Windows.Forms;
using UnityWcfExtensionsTestClient.Service;

namespace UnityWcfExtensionsTestClient
{
    /// <summary>
    /// Main application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Variable for the service host.
        /// </summary>
        private static ServiceHost serviceHost;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Program.serviceHost = new ServiceHost(typeof(UnityService));
            serviceHost.Open();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Occurs when the application is about to shut down.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains no event data.</param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Program.serviceHost != null)
            {
                Program.serviceHost.Close();
                Program.serviceHost = null;
            }
        }
    }
}
