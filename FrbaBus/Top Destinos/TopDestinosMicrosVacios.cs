using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Top_Destinos
{
    public partial class TopDestinosMicrosVacios : Form1
    {
        public TopDestinosMicrosVacios(string unAño, int unSemestre)
        {            
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                DataTable tabla = new DataTable();
                int mesFinal;
                int mesInicial;

                if (unSemestre == 1)
                {
                    mesInicial = 1;
                    mesFinal = 6;
                }
                else
                {
                    mesInicial = 7;
                    mesFinal = 12;
                }

                cargarATablaParaDataGripView("USE GD1C2013 select * from LOS_VIAJEROS_DEL_ANONIMATO.FTOP5MicrosVaciosDestinos('" + unAño + "'," + mesInicial + "," + mesFinal + ")", ref tabla, conexion);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[1].HeaderText = "Promedio de butacas vacias";
            }
        }
    }
}
