using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DisableSS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            if(mutex.WaitOne(TimeSpan.Zero, true)) {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            } else {
                //MessageBox.Show("only one instance at a time");
                //Make this app the front of the z-order
            }
        }
    }
}
