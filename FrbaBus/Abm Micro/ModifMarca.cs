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
    public partial class ModifMarca : Form1
    {
        public ModifMarca(string unaPatente)
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                
                
                conexion.Open();
                SqlCommand marca = new SqlCommand("USE GD1C2013 select ma.Marca from los_viajeros_del_anonimato.MICRO mi join LOS_VIAJEROS_DEL_ANONIMATO.MARCA ma on (mi.Marca = ma.Id_Marca) WHERE Patente = '" + unaPatente + "'", conexion);
                string marcaActual = marca.ExecuteScalar().ToString();

                textBox1.Text = marcaActual;
                textBox1.Enabled = false;

                textBox3.Text = unaPatente;
                textBox3.Enabled = false;

                SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT Marca FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Marcas  () ORDER BY RN", conexion);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tablaDeNombres = new DataTable();

                adapter.Fill(tablaDeNombres);

                comboBox1.DisplayMember = "Marca";
                comboBox1.DataSource = tablaDeNombres;  

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            comboBox1.Text = "No seleccionado";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string patente = textBox3.Text;
                string nuevoNombreMarca = textBox2.Text;
                string marcaActual = textBox1.Text;
                string otraMarca = comboBox1.Text;

                using (SqlConnection conexion = this.obtenerConexion())
                {

                    if ((nuevoNombreMarca.Length > 0) && !(String.Equals(comboBox1.Text, "No seleccionado")))
                    {
                        throw new Exception("Debe hacer solo una accion a la vez");
                    }

                    if ((nuevoNombreMarca.Length == 0) && (String.Equals(comboBox1.Text, "No seleccionado")))
                    {
                        throw new Exception("No especifica que hacer");
                    }
                   
                    if (nuevoNombreMarca.Length > 0)
                    {
                        conexion.Open();
                        SqlCommand modificarNombre = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.MARCA SET Marca = 'asd' where MARCA= '" + marcaActual + "'", conexion);
                        modificarNombre.ExecuteNonQuery();

                        conexion.Close();
                        textBox1.Text = otraMarca;
                        (new Dialogo("Marca modificada;" +marcaActual+ " modificada a "+nuevoNombreMarca, "Aceptar")).ShowDialog();

                    }

                    if (!(String.Equals(comboBox1.Text, "No seleccinado")))
                    {
                        using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPModificarMarca", conexion))
                        {
                            conexion.Open();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Patente", SqlDbType.NVarChar).Value = patente;                            
                            cmd.Parameters.Add("@otraMarca", SqlDbType.NVarChar).Value = otraMarca;
                          
                            cmd.ExecuteNonQuery();

                            (new Dialogo("Marca modificada;" +marcaActual+ " modificada a "+otraMarca+ " para la patente "+patente, "Aceptar")).ShowDialog();

                            textBox1.Text = otraMarca;
                        }
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
