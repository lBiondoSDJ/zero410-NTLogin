using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace NTLogin
{
    public class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main(string[] args)
        {
            Process ninjaProc = null;
            bool foundNinja = false;
            string userName = string.Empty, passwd = string.Empty;
            string ninjaExe = @"C:\Program Files\NinjaTrader 8\bin\NinjaTrader.exe";

            if (args.Length == 2)
            {
                userName = args[0];
                passwd = args[1];
            }      
            else if (args.Length == 3) 
            {
                userName = args[0];
                passwd = args[1];
                ninjaExe = args[2];
            }

            Process.Start(@ninjaExe);

            while (!foundNinja)
            {
                Process[] processlist = Process.GetProcessesByName("NinjaTrader");
                if (processlist.Length > 0)
                {
                    ninjaProc = processlist[0];
                    foundNinja = true;
                }

                System.Threading.Thread.Sleep(100);
            }

            System.Threading.Thread.Sleep(5000);

            SetForegroundWindow(ninjaProc.MainWindowHandle);
            // zero410 fix (D13): DPI/multi-monitor-independent focus via TAB sequence.
            // Original used coordinate-based ButtonClick(Left+30, Top+210) which fails
            // on high-DPI displays (e.g. 150% scaling). Tab order verified: 3 TAB land
            // focus on password field.
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            Clipboard.SetText(passwd);
            SendKeys.SendWait("^v");
            SendKeys.SendWait("{ENTER}");
        }
    }
}
