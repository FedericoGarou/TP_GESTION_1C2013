using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Top_Micros
{
    public partial class TopMicrosMayorPeriodoBaja : Form1
    {
        public TopMicrosMayorPeriodoBaja()
        {
            InitializeComponent();

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                
                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 SELECT * from LOS_VIAJEROS_DEL_ANONIMATO_FTOP5MicrosMayorPeriodoBaja()", ref tabla, conexion);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[1].HeaderText = "Cantidad de dias en baja";
            }
        }
    }
}
