﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Login;
using FrbaBus.Compra_de_Pasajes;//Borrar
using FrbaBus.Abm_Micro;// Borrar



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
            
        }

        
    }
}
