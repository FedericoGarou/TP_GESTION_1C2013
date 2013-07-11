using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Abm_Recorrido;

namespace FrbaBus.GenerarViaje
{
    public partial class Generar_Viaje : Form1
    {
        public Generar_Viaje()
        {
            DateTime fechaActual = getFechaActual();
            
            InitializeComponent();
            // Llenar combo box con valores posibles
            using (SqlConnection conexion = this.obtenerConexion())
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

                comboBox4.Text="No Seleccionado";
            }
        }            
        

        private void sePuedeCrearUnViaje()
        {
            String errorMensaje = "";
            bool hayError = false;
            DateTime fechaActual = getFechaActual();

            // Fecha viaje posterior a Fecha Actual
            if (fechaActual > dateTimePicker1.Value.Date)
            {
                hayError = true;
                errorMensaje += "La fecha seleccionada es anterior a la fecha actual;";
            }
            
            //a igual fecha comprobar que el horario es posterior
            if (fechaActual == dateTimePicker1.Value.Date)
            {
                if (dateTimePicker1.Value.Hour == numericUpDown1.Value)
                {
                    if (dateTimePicker1.Value.Minute >= numericUpDown2.Value)
                    {
                        hayError = true;
                        errorMensaje += "La fecha seleccionada es anterior a la fecha actual;";
                    }
                }
                else
                {
                    if (dateTimePicker1.Value.Hour > numericUpDown1.Value)
                    {
                        hayError = true;
                        errorMensaje += "La fecha seleccionada es anterior a la fecha actual;";
                    }
                }
               
            }


            // Fecha llegada posterior o igual a fecha salida
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                if (numericUpDown1.Value == numericUpDown3.Value)
                {
                    if (numericUpDown2.Value == numericUpDown4.Value)
                    {
                        hayError = true;
                        errorMensaje += "La fecha de salida es igual a la fecha de llegada estimada;";
                    }
                    else
                    {
                        if (numericUpDown2.Value > numericUpDown4.Value)
                        {
                            hayError = true;
                            errorMensaje += "La fecha de salida es posterior a la fecha de llegada estimada;";
                        }
                    }
                }else
                {
                    if(numericUpDown1.Value > numericUpDown3.Value)
                    {
                    hayError = true;
                    errorMensaje += "La fecha de salida es posterior a la fecha de llegada estimada;";
                    }
                }
            }
            else
            {
                if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
                {
                    hayError = true;
                    errorMensaje += "La fecha de salida es posterior a la fecha de llegada estimada;";
                }
            }
            
            // Los campos origen y destino son iguales.
            if (comboBox1.Text.Equals(comboBox2.Text))
            {
                hayError = true;
                errorMensaje += "El origen y el destino son el mismo;";
            }

            if (comboBox1.Text.Equals("No seleccionado") ||
                 comboBox2.Text.Equals("No seleccionado") ||
                 comboBox3.Text.Equals("No seleccionado") ||
                 comboBox4.Text.Equals("No seleccionado"))
            {
                hayError = true;
                errorMensaje += "Alguno de los campos necesarios no fue seleccionado;";
            }
                                 
            // Que el recorrido no exista en la base de datos

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPexisteElRecorrido", conexion))
                {
                    conexion.Open();

                    bool existeRecorrido;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Origen", SqlDbType.NVarChar).Value = comboBox1.Text;
                    cmd.Parameters.Add("@Destino", SqlDbType.NVarChar).Value = comboBox2.Text;
                    cmd.Parameters.Add("@Servicio", SqlDbType.NVarChar).Value = comboBox3.Text;
                    cmd.Parameters.Add("@retorno", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    existeRecorrido = Convert.ToBoolean(cmd.Parameters["@retorno"].Value);
                    if (!existeRecorrido)
                    {
                        hayError = true;
                        errorMensaje += "No existe el recorrido;";
                    }
                }
            }

            //comprobar que en la fecha este disponible el micro
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPMicroOcupadoEnFecha", conexion))
                {
                    conexion.Open();

                   // bool microOcupado;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PatenteMicro", SqlDbType.NVarChar).Value = comboBox4.Text;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dateTimePicker1.Value;                   
                    cmd.Parameters.Add("@retorno", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int microOcupado = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                    if (microOcupado == 1)
                    {
                        hayError = true;
                        errorMensaje += "Micro no disponible para la fecha " + dateTimePicker1.Value + ";Motivo: ya hay un viaje ingresado en esa fecha";
                    }

                    if (microOcupado == 2)
                    {
                        hayError = true;
                        errorMensaje += "Micro no disponible para la fecha " + dateTimePicker1.Value + ";Motivo: el micro esta fuera de servicio en esa fecha";
                    }
                }
            }


            if (hayError)
                throw new Exception(errorMensaje);                
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                //cargar comboBox con micros disponibles
                string tipoServicio = comboBox3.Text;
                conexion.Open();

                SqlCommand cmd = new SqlCommand("USE GD1C2013 select DISTINCT(m.Patente) from LOS_VIAJEROS_DEL_ANONIMATO.MICRO m join LOS_VIAJEROS_DEL_ANONIMATO.VIAJE v on (m.Patente=v.PatenteMicro) where TipoServicio='" + tipoServicio + "' and BajaPorVidaUtil=0", conexion);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tablaDeNombres = new DataTable();

                adapter.Fill(tablaDeNombres);

                comboBox4.DisplayMember = "Patente";
                comboBox4.DataSource = tablaDeNombres;

            }
        }

        //boton limpiar
        private void button3_Click(object sender, EventArgs e)
        {            
            comboBox1.Text = "No seleccionado";
            comboBox2.Text = "No seleccionado";
            comboBox3.Text = "No seleccionado";
            comboBox4.Text = "No seleccionado";
        }

        
        //boton crear
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string patenteMicro = comboBox4.Text;
                string origen = comboBox1.Text;
                string destino = comboBox2.Text;
                string tipoServicio = comboBox3.Text;
               
                double horasSalida = Convert.ToDouble(numericUpDown1.Value.ToString());
                double minutosSalida = Convert.ToDouble(numericUpDown2.Value.ToString());
                DateTime fechaSalida = dateTimePicker1.Value;
                DateTime fechaSalidaSinTiempo = fechaSalida.AddSeconds(-fechaSalida.Second).AddMinutes(-fechaSalida.Minute).AddHours(-fechaSalida.Hour);
                DateTime fechaSalidaCompleta = fechaSalidaSinTiempo.AddHours(horasSalida).AddMinutes(minutosSalida);

                double horasLlegada = Convert.ToDouble(numericUpDown3.Value.ToString());
                double minutosLlegada = Convert.ToDouble(numericUpDown4.Value.ToString());
                DateTime fechaLlegada = dateTimePicker2.Value;
                DateTime fechaLlegadaSinTiempo = fechaLlegada.AddSeconds(-fechaLlegada.Second).AddMinutes(-fechaLlegada.Minute).AddHours(-fechaLlegada.Hour);
                DateTime fechaLlegadaCompleta = fechaLlegadaSinTiempo.AddHours(horasLlegada).AddMinutes(minutosLlegada);
                
                 
                this.sePuedeCrearUnViaje();                

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SPCrearViaje", conexion))
                    {
                        conexion.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PatenteMicro", SqlDbType.NVarChar).Value = patenteMicro;
                        cmd.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = fechaSalidaCompleta;
                        cmd.Parameters.Add("@FechaLlegadaEstimada", SqlDbType.DateTime).Value = fechaLlegadaCompleta;
                        cmd.Parameters.Add("@Origen", SqlDbType.NVarChar).Value = origen;
                        cmd.Parameters.Add("@Destino", SqlDbType.NVarChar).Value = destino;
                        cmd.Parameters.Add("@TipoServicio", SqlDbType.NVarChar).Value = tipoServicio;

                        cmd.ExecuteNonQuery();

                        (new Dialogo("Nuevo viaje creado;Micro: " +patenteMicro+ ";Fecha salida: " +fechaSalidaCompleta+ ";Fecha llegada estimada: " +fechaLlegadaCompleta+ ";Origen: " +origen+ ";Destino: " +destino+ ";Tipo Servicio: " +tipoServicio, "Aceptar")).ShowDialog();
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
