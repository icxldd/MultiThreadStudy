using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSample
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if DEBUG
            Shell.AllocConsole();
            Shell.WriteLine("ע�⣺��������...");

            Shell.WriteLine("/tWritten by Oyi319");
            Shell.WriteLine("{0}��{1}", "����", "����һ��������Ϣ��");
            Shell.WriteLine("{0}��{1}", "����", "����һ��������Ϣ��");
            Shell.WriteLine("{0}��{1}", "ע��", "����һ����Ҫ��ע����Ϣ��");
            Shell.WriteLine("");
#endif
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

#if DEBUG
            Shell.WriteLine("ע�⣺2���ر�...");
            Thread.Sleep(2000);
            Shell.FreeConsole();
#endif
        }
    }
}
