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

                    DataTable puntos = new DataTable();
                    DataTable puntosCanjeados = new DataTable();
                    DateTime fechaMenosUnAño = (getFechaActual().AddYears(-1));                    
                    /*
                    SqlCommand borrarPuntosVencidos = new SqlCommand("USE GD1C2013 DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni + "and Fecha < '" + fechaMenosUnAño + "'", conexion);
                    borrarPuntosVencidos.ExecuteNonQuery();
                    */
                    cargarATablaParaDataGripView("USE GD1C2013 SELECT Puntos, Fecha, CodigoCompra FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni + " and CodigoCanje is NULL and Fecha > '" + fechaMenosUnAño + "' order by 2", ref puntos, conexion);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = puntos;

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT SUM(Puntos) FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni + "and CodigoCanje is NULL and Fecha > '" + fechaMenosUnAño +"'", conexion);
                    string totalPuntos = cmd.ExecuteScalar().ToString();

                    textBox2.Text = totalPuntos;

                    cargarATablaParaDataGripView("USE GD1C2013 select p.Puntos, p.Fecha as FechaObtencion, c.Fecha as FechaCanje, pr.DetalleProducto from LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF p join LOS_VIAJEROS_DEL_ANONIMATO.CANJE c on (p.CodigoCanje = c.CodigoCanje) join LOS_VIAJEROS_DEL_ANONIMATO.PREMIO pr on (c.CodigoProducto = pr.CodigoProducto) where p.DNI_Usuario = " + dni + " and p.Fecha > '" + fechaMenosUnAño +"' order by 3", ref puntosCanjeados, conexion);

                    dataGridView2.Columns.Clear();
                    dataGridView2.DataSource = puntosCanjeados;

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
