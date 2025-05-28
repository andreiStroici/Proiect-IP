/*************************************************************************
 *                                                                       *
 *  File:        Program.cs                                              *
 *  Copyright:   (c) 2025, A. Denisa                                     *
 *                                                                       *
 *  Description: Programul principal.                                    *
 *                                                                       *
 *                                                                       *
 *  This code and information is provided "as is" without warranty of    *
 *  any kind, either expressed or implied, including but not limited     *
 *  to the implied warranties of merchantability or fitness for a        *
 *  particular purpose. You are free to use this source code in your     *
 *  applications as long as the original copyright notice is included.   *
 *                                                                       *      
 ************************************************************************ */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string relativePath = @"..\..\..\ClientBackend\bin\Debug\ClientBackend.exe";
            string workerPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
            bool showConsole = true;

            var startInfo = new ProcessStartInfo
            {
                FileName = workerPath,
                UseShellExecute = false,
                CreateNoWindow = !showConsole,
                WindowStyle = showConsole ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden
            };

            Process proc = Process.Start(startInfo);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainView());

            proc.Kill();
        }
    }
}
