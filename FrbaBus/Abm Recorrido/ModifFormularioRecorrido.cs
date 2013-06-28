using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ModifFormularioRecorrido : Form1
    {
        private Boolean Habilitacion;
        private Boolean HabilitacionOriginal;

        public ModifFormularioRecorrido(String origen,String destino,String Servicio)
        {
            InitializeComponent();

            comboBox1.Text = origen;
            comboBox1.Enabled = false;
            comboBox2.Text = destino;
            comboBox2.Enabled = false;
            comboBox3.Text = Servicio;
            comboBox3.Enabled = false;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerDatosDeRecorrido",conexion))
                {
                    conexion.Open();
                
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@Origen", SqlDbType.NVarChar).Value = origen;
                    comando.Parameters.Add("@Destino", SqlDbType.NVarChar).Value = destino;
                    comando.Parameters.Add("@Servicio", SqlDbType.NVarChar).Value = Servicio;
                    comando.Parameters.Add("@Habilitacion", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("@PrecioBase_KG", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("@PrecioBase", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    numericUpDown1.Value = Convert.ToDecimal(comando.Parameters["@PrecioBase"].Value);
                    numericUpDown2.Value = Convert.ToDecimal(comando.Parameters["@PrecioBase_KG"].Value);
                    Habilitacion = Convert.ToBoolean(comando.Parameters["@Habilitacion"].Value);
                    HabilitacionOriginal = Habilitacion;

                    if (Habilitacion)
                        Habilitado.Checked = true;
                    else
                        Deshabilitado.Checked = true;
                    
                }
            }

        }

        // Previsualizar recorrido
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeModificarUnRecorrido();

                this.Hide();
                new VisualizarRecorrido("Previsualizar",
                                        comboBox1.Text.ToString(),
                                        comboBox2.Text.ToString(),
                                        comboBox3.Text.ToString(),
                                        numericUpDown1.Value,
                                        numericUpDown2.Value,
                                        Habilitacion).ShowDialog();
                this.Show();
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
            
            
        }

        // Validaciones
        private void sePuedeModificarUnRecorrido()
        {
            String errorMensaje = "";
            bool hayError = false;

            // Los precios no son cero
            if (numericUpDown1.Value <= 0)
            {
                hayError = true;
                errorMensaje += "Error en el precio base para pasaje;";
            }

            if (numericUpDown2.Value <= 0)
            {
                hayError = true;
                errorMensaje += "Error en el precio base por Kg.;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        } 

        // Modificar en la base de datos un recorrido
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeModificarUnRecorrido();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ModificarRecorrido", conexion))
                    {
                        conexion.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@origen", SqlDbType.NVarChar).Value = comboBox1.Text;
                        cmd.Parameters.Add("@destino", SqlDbType.NVarChar).Value = comboBox2.Text;
                        cmd.Parameters.Add("@servicio", SqlDbType.NVarChar).Value = comboBox3.Text;
                        cmd.Parameters.Add("@basePasaje", SqlDbType.Decimal).Value = numericUpDown1.Value;
                        cmd.Parameters.Add("@baseKG", SqlDbType.Decimal).Value = numericUpDown2.Value;

                        if (Habilitacion)
                            cmd.Parameters.Add("@habilitacion", SqlDbType.Bit).Value = 1;
                        else
                            cmd.Parameters.Add("@habilitacion", SqlDbType.Bit).Value = 0;

                        cmd.ExecuteNonQuery();

                        this.Hide();
                        new VisualizarRecorrido("Recorrido Modificado",
                                        comboBox1.Text.ToString(),
                                        comboBox2.Text.ToString(),
                                        comboBox3.Text.ToString(),
                                        numericUpDown1.Value,
                                        numericUpDown2.Value,
                                        Habilitacion).ShowDialog();
                        this.Show();
                    }
                }

            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void Habilitado_CheckedChanged(object sender, EventArgs e)
        {
            Habilitacion = true;
        }

        private void Deshabilitado_CheckedChanged(object sender, EventArgs e)
        {
            Habilitacion = false;
        }


        
    }
}
