using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModelLib.MotionBlurWindow
{
    public class SingleInstanceApplicationWrapper : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        public SingleInstanceApplicationWrapper()
        {
            // Enable single-instance mode.
            this.IsSingleInstance = true;
        }
        // Create the WPF application class.
        public Window app;
        protected override bool OnStartup(
        Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            app = new Window();
            app.Show();
            return false;
        }

        // Direct multiple instances.
        protected override void OnStartupNextInstance(
        Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs e)
        {
            if (e.CommandLine.Count > 0)
            {
                app.Show();
            }
        }
    }
}
