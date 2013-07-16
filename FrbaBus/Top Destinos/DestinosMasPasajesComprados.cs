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
    public partial class DestinosMasPasajesComprados : Form1
    {
        public DestinosMasPasajesComprados(string unAño, int unSemestre)
        {
            InitializeComponent();

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 select TOP 5 NombreCiudad, LOS_VIAJEROS_DEL_ANONIMATO.FcalcularPasajesCompradosEn(NombreCiudad," + unAño + "," + unSemestre + ") AS cantidadPasajes from LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD where LOS_VIAJEROS_DEL_ANONIMATO.FcalcularPasajesCompradosEn(NombreCiudad," + unAño + "," + unSemestre + ")!=0 order by 2 desc", ref tabla, conexion);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[1].HeaderText = "Pasajes comprados";
            }
        }
    }
}
