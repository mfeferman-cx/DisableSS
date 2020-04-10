using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DisableSS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);
        private void Form1_Load(object sender, EventArgs e)
        {
            SetThreadExecutionState(
                EXECUTION_STATE.ES_AWAYMODE_REQUIRED
                |EXECUTION_STATE.ES_CONTINUOUS
                |EXECUTION_STATE.ES_DISPLAY_REQUIRED
                |EXECUTION_STATE.ES_SYSTEM_REQUIRED
                );
        }
    }
}
