using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace WBD_GUI
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form4 form4 = new Form4();
            Application.Run(form4);
            //Application.Run(new Form1(form4.getLogin(),form4.getPassword()));




        }
    }
}
