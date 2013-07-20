using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Micro
{
    public partial class AgregarButaca : Form1
    {
        public AgregarButaca(string unaPatente)
        {
            InitializeComponent();
            textBox1.Text = unaPatente;
            textBox1.Enabled = false;
                        
            comboBox1.Items.Add("Pasillo");
            comboBox1.Items.Add("Ventanilla");
            comboBox1.Text="No seleccionado";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT DISTINCT(Piso) FROM LOS_VIAJEROS_DEL_ANONIMATO.F_PisosBucatasDe ('" + unaPatente + "')", conexion);                     

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tablaDeNombres = new DataTable();

                adapter.Fill(tablaDeNombres);

                comboBox2.DisplayMember = "Piso";
                comboBox2.DataSource = tablaDeNombres;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "No seleccionado";
            comboBox2.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (String.Equals(comboBox1.Text, "No seleccionado") || String.Equals(comboBox2.Text, "0"))
                {
                    throw new Exception("Debe completar todos los campos");
                }

                using (SqlConnection conexion = this.obtenerConexion())
                {

                    using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPInsertarButaca", conexion))
                    {
                        conexion.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Patente", SqlDbType.NVarChar).Value = textBox1.Text;
                        cmd.Parameters.Add("@Ubicacion", SqlDbType.NVarChar).Value = comboBox1.Text;
                        cmd.Parameters.Add("@Piso", SqlDbType.NVarChar).Value = comboBox2.Text;

                        cmd.ExecuteNonQuery();

                        (new Dialogo("Butaca agregada;Piso: " + comboBox2.Text + " ubicacion: " + comboBox1.Text + " para la patente: " + textBox1.Text, "Aceptar")).ShowDialog();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }
    }
}
