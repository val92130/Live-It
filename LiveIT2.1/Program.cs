using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Thread t = new Thread(() =>
            {
                Application.Run(new SplashScreen());
            });

            t.Start();
            Thread.Sleep(3000);
            t.Abort();

            Application.Run( new Menu() );

        }
        
    }
}
