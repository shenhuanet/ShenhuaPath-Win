using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShenhuaPath
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            String[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                String path = "";
                for (int i = 1; i < args.Length; i++)
                {
                    path = path + args[i] + " ";
                }
                Clipboard.SetText(path.Trim());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ShenhuaPath());
            }
        }
    }
}
