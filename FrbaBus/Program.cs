using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Login;


namespace FrbaBus
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
            Application.SetCompatibleTextRenderingDefault(false);       

            Application.Run(new Pantalla_Inicial());
           

            //Application.Run(new Generar_Viaje());
           


        }

        
    }
}
