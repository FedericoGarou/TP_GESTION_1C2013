using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Consulta_Puntos_Adquiridos
{
    public partial class ConsultaPuntos : Form1
    {
        public ConsultaPuntos()
        {
            InitializeComponent();
            
            textBox2.Enabled = false;

     

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dni = textBox1.Text;
          


            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    if (!(Convert.ToInt64(textBox1.Text) > 0))
                    {
                        throw new Exception("DNI invalido;");
                    }
                                
                    conexion.Open();
                    DataTable tabla = new DataTable();

                    DateTime fechaMenosUnAño = (getFechaActual().AddYears(-1));

                    (new Dialogo("ERROR - " + fechaMenosUnAño, "Aceptar")).ShowDialog();

                    SqlCommand borrarPuntosVencidos = new SqlCommand("USE GD1C2013 DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni + "and Fecha < '" + fechaMenosUnAño + "'", conexion);
                    borrarPuntosVencidos.ExecuteNonQuery();
                    
                    cargarATablaParaDataGripView("USE GD1C2013 SELECT Puntos, Fecha, CodigoCompra, CodigoCanje FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni, ref tabla, conexion);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT SUM(Puntos) FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni, conexion);
                    string totalPuntos = cmd.ExecuteScalar().ToString();

                    textBox2.Text = totalPuntos;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }
    }
}
