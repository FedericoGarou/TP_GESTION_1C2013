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
        public TopMicrosMayorPeriodoBaja(string unAño, int unSemestre)
        {
            InitializeComponent();

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                
                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 select TOP 5 Patente, LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(Patente," + unAño + "," + unSemestre + ") AS cantidadDias from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio where LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(Patente," + unAño + "," + unSemestre + ")!=0 group by Patente order by cantidadDias desc", ref tabla, conexion);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[1].HeaderText = "Cantidad de dias en baja";
            }
        }
    }
}
