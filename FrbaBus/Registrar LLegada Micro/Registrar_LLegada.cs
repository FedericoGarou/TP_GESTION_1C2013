using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Registrar_LLegada_Micro
{
    public partial class Registrar_LLegada : Form1
    {
        public Registrar_LLegada(string codigoViaje)
        {
            InitializeComponent();
            label7.Text = codigoViaje;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT FechaLlegadaEstimada FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE where CodigoViaje='" + codigoViaje + "'", conexion);
                    DateTime FechaLLegadaEstimada = (DateTime)cmd.ExecuteScalar();

                    textBox1.Text = FechaLLegadaEstimada.ToString();
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }


        //boton calcular
        private void button2_Click(object sender, EventArgs e)
        {
            double horas = Convert.ToDouble(numericUpDown1.Value.ToString());
            double minutos = Convert.ToDouble(numericUpDown2.Value.ToString());
            string codigoViaje = label7.Text;

            DateTime fechaLLegadaEstimada = Convert.ToDateTime(textBox1.Text);
            DateTime fechaExacta = fechaLLegadaEstimada.AddHours(horas).AddMinutes(minutos);

            textBox2.Text = fechaExacta.ToString();

        }


        //boton volver a calcular
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }


        //boton registrar
        private void button1_Click(object sender, EventArgs e)
        {
            string codigoViaje = label7.Text;
            DateTime fechaExacta = Convert.ToDateTime(textBox2.Text);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPRegistrarLLegada", conexion))
                {
                    conexion.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodigoViaje", SqlDbType.NVarChar).Value = codigoViaje;
                    cmd.Parameters.Add("@FechaExacta", SqlDbType.DateTime).Value = fechaExacta;

                    cmd.ExecuteNonQuery();
                    (new Dialogo("Fecha de llegada " + fechaExacta.ToString() + " regitrada para el viaje " + codigoViaje, "Aceptar")).ShowDialog();
                }

                /*
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("declare @fechaExacta datetime="+fechaExacta+" USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.VIAJE SET FechaLlegada=@fechaExacta WHERE CodigoViaje='" + codigoViaje + "'", conexion);


                    (new Dialogo("Fecha de llegada "+fechaExacta.ToString()+" regitrada para el viaje "+codigoViaje, "Aceptar")).ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
                 */
            }
        }
    }
}