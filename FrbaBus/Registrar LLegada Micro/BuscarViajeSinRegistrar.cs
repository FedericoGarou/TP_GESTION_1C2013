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
    public partial class BuscarViajeSinRegistrar : Form1
    {
        public BuscarViajeSinRegistrar()
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

                comboBox4.Text = "No Seleccionado";
            }
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

        private void button1_Click(object sender, EventArgs e)
        {



            //cargarATablaParaDataGripView(
            try
            {
                string patenteMicro = comboBox4.Text;
                string origen = comboBox1.Text;
                string destino = comboBox2.Text;
                string tipoServicio = comboBox3.Text;

                // this.sePuedeCrearUnViaje();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    // SqlCommand recorrido = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Recorrido WHERE CiudadOrigen = '" + origen + "' and CiudadDestino = '" + destino + "' and TipoServicio = '" + tipoServicio + "'", conexion);
                    // Numeric(18,0) codigoRecorrido = recorrido.ExecuteScalar().ToString();

                    DataTable tabla = new DataTable();
                    cargarATablaParaDataGripView("declare @codigo numeric(18,0) = (LOS_VIAJEROS_DEL_ANONIMATO.F_ObetenerRecorrido('FWX-674', 'Santa Fe', 'Corrientes', 'Ejecutivo')) SELECT CodigoViaje, FechaSalida, PatenteMicro, FechaLlegadaEstimada, FechaLlegada FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE WHERE CodigoRecorrido = @codigo and PatenteMicro= '" + patenteMicro + "'", ref tabla, conexion);
                    
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    DataGridViewButtonColumn botonRegistrarLLegada = this.crearBotones("", "RegistrarLLegada");
                    dataGridView1.Columns.Add(botonRegistrarLLegada);

                }
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }




        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String codigoViaje = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                // DateTime fechaEstimadaLLegada = dataGridView1.Rows[e.RowIndex].Cells[3].Value;

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();

                    if (e.ColumnIndex == 5)
                    {
                        new Registrar_LLegada(codigoViaje).Show();
                    }
                }


            }
        }

        

    }
}
