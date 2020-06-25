using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DisableSS
{
    static class Program
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        static Mutex mutex = new Mutex(false, "Jenny-Jenny-867-5309");
        
        static readonly int SWP_NOSIZE = 0x0001;
        static readonly int SWP_SHOWWINDOW = 0x0040;
        static readonly int SWP_NOMOVE = 0x0002;

        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
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
            }
            else
            {
                // bring the existing instance to the foreground
                Process[] ps = Process.GetProcesses(".");
                foreach (Process p in ps) {
                    if (p.ProcessName == "DisableSS" && p.MainWindowHandle.ToInt32() != 0) {
                        if (IsIconic(p.MainWindowHandle))
                            ShowWindow(p.MainWindowHandle, 1);                            
                        else
                        {
                            
                            //A little trick here to make it the utmost top window and then 
                            //set it to not be the utmost top, so that it can be hidden again...
                            GetWindowRect(p.MainWindowHandle, out RECT theRect);
                            SetWindowPos(p.MainWindowHandle, -1, theRect.left, theRect.top, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                            SetWindowPos(p.MainWindowHandle, -2, theRect.left, theRect.top, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                        }
                        break;                        
                    }
                }
            }
        }
    }
}
