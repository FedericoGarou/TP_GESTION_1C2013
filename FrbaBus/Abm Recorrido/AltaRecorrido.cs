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
    public partial class AltaRecorrido : Form1
    {
        public AltaRecorrido()
        {
            InitializeComponent();

            // Llenar combo box con valores posibles
            using ( SqlConnection conexion = this.obtenerConexion() )
            {
                conexion.Open();
                SqlCommand cmdParaLlenarComboBox = new SqlCommand();
                cmdParaLlenarComboBox.Connection = conexion;
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmdParaLlenarComboBox);

                // Llenar los combo box 'origen' y 'destino'

                cmdParaLlenarComboBox.CommandText = "USE GD1C2013 SELECT NombreCiudad FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Ciudades () ORDER BY RN";
                
                DataTable ciudadesOrigen = new DataTable();
                adapter.Fill(ciudadesOrigen);
                comboBox1.DisplayMember = "NombreCiudad";
                comboBox1.DataSource = ciudadesOrigen;

                DataTable ciudadesDestino = new DataTable();
                adapter.Fill(ciudadesDestino);
                comboBox2.DisplayMember = "NombreCiudad";
                comboBox2.DataSource = ciudadesDestino;

                // Llenar el combo box 'tipo de servicio'

                cmdParaLlenarComboBox.CommandText = "USE GD1C2013 SELECT NombreServicio FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Servicios () ORDER BY RN";
                DataTable tiposDeServicio = new DataTable();
                adapter.Fill(tiposDeServicio);
                comboBox3.DisplayMember = "NombreServicio";
                comboBox3.DataSource = tiposDeServicio; // Tipos de servicio
            }
        }

        // Previsualizar recorrido
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeCrearUnRecorrido();

                this.Hide();
                new VisualizarRecorrido("Previsualizar",
                                        comboBox1.Text.ToString(),
                                        comboBox2.Text.ToString(),
                                        comboBox3.Text.ToString(),
                                        numericUpDown1.Value,
                                        numericUpDown2.Value).ShowDialog();
                this.Show();
            }
            catch(ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
                this.Show();
            }
            
            
        }

        // Validaciones
        private void sePuedeCrearUnRecorrido()
        {
            String errorMensaje = "";
            bool hayError = false;

            // Los campos origen y destino son distintos.
            if (comboBox1.Text.Equals(comboBox2.Text))
            {
                hayError = true;
                errorMensaje += "El origen y el destino son el mismo;";
            }

            if ( comboBox1.Text.Equals("No seleccionado") ||
                 comboBox2.Text.Equals("No seleccionado") ||
                 comboBox3.Text.Equals("No seleccionado") )
            {
                hayError = true;
                errorMensaje += "Alguno de los campos necesarios no fue seleccionado;";
            }

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

            // Que el recorrido no exista en la base de datos

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPexisteElRecorrido", conexion))
                {
                    conexion.Open();

                    bool existeRecorrido;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Origen",SqlDbType.NVarChar).Value = comboBox1.Text;
                    cmd.Parameters.Add("@Destino", SqlDbType.NVarChar).Value = comboBox2.Text;
                    cmd.Parameters.Add("@Servicio", SqlDbType.NVarChar).Value = comboBox3.Text;
                    cmd.Parameters.Add("@retorno", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    existeRecorrido = Convert.ToBoolean(cmd.Parameters["@retorno"].Value);
                    if (existeRecorrido)
                    {
                        hayError = true;
                        errorMensaje += "Ya existe el recorrido;";
                    }
                    
                }
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        } 

        // Limpiar campos
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }

        // Dar de alta en la base de datos un recorrido
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeCrearUnRecorrido();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.insertarRecorrido", conexion))
                    {
                        conexion.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@origen", SqlDbType.NVarChar).Value = comboBox1.Text;
                        cmd.Parameters.Add("@destino", SqlDbType.NVarChar).Value = comboBox2.Text;
                        cmd.Parameters.Add("@servicio", SqlDbType.NVarChar).Value = comboBox3.Text;
                        cmd.Parameters.Add("@basePasaje", SqlDbType.Decimal).Value = numericUpDown1.Value;
                        cmd.Parameters.Add("@baseKG", SqlDbType.Decimal).Value = numericUpDown2.Value;

                        cmd.ExecuteNonQuery();

                        this.Hide();
                        new VisualizarRecorrido("Recorrido agregado",
                                        comboBox1.Text.ToString(),
                                        comboBox2.Text.ToString(),
                                        comboBox3.Text.ToString(),
                                        numericUpDown1.Value,
                                        numericUpDown2.Value).ShowDialog();
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
    }
}
