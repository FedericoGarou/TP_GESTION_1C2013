using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ListadoRol : Form1
    {
        public ListadoRol()
        {
            InitializeComponent();

            DataGridViewButtonColumn botonesBajaCiudad = this.crearBotones("Eliminar Ciudad", "Eliminar");
            dataGridView1.Columns.Add(botonesBajaCiudad);
            dataGridView1.Columns[0].Visible = false;
            DataGridViewButtonColumn botonesModifCiudad = this.crearBotones("Modificar Ciudad", "Modificar");
            dataGridView1.Columns.Add(botonesModifCiudad);
            dataGridView1.Columns[1].Visible = false;

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
                comboOrigen.DisplayMember = "NombreCiudad";
                comboOrigen.DataSource = ciudadesOrigen;
                
                DataTable ciudadesDestino = new DataTable();
                adapter.Fill(ciudadesDestino);
                comboDestino.DisplayMember = "NombreCiudad";
                comboDestino.DataSource = ciudadesDestino;

                // Llenar el combo box 'tipo de servicio'

                cmdParaLlenarComboBox.CommandText = "USE GD1C2013 SELECT NombreServicio FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Servicios () ORDER BY RN";
                DataTable tiposDeServicio = new DataTable();
                adapter.Fill(tiposDeServicio);
                comboServicio.DisplayMember = "NombreServicio";
                comboServicio.DataSource = tiposDeServicio; // Tipos de servicio

                comboHabilitación.Items.Add("No seleccionado");
                comboHabilitación.Items.Add("Deshabilitados");
                comboHabilitación.Items.Add("Habilitados");
                comboHabilitación.SelectedIndex = 0;

            }
        }

        //Limpiar
        private void button2_Click(object sender, EventArgs e)
        {
            comboOrigen.SelectedIndex = 0;
            comboDestino.SelectedIndex = 0;
            comboServicio.SelectedIndex = 0;
            comboHabilitación.SelectedIndex = 0;
            numericPrecioDesde.Value = 0;
            numericPrecioHasta.Value = 0;
            numericPrecioPaqueteDesde.Value = 0;
            numericPrecioPaqueteHasta.Value = 0;
            dataGridView1.DataSource = null;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            
        }

        // Validaciones
        private void validarFiltros()
        {
            String errorMensaje = "";
            bool hayError = false;

            if (numericPrecioDesde.Value < 0 || numericPrecioHasta.Value < 0)
            {
                hayError = true;
                errorMensaje += "El precio no debe ser menor a 0;";
            }

            if (numericPrecioDesde.Value > numericPrecioHasta.Value)
            {
                hayError = true;
                errorMensaje += "Pasaje: El precio mínimo debe ser menor a precio máximo;";
            }

            if (numericPrecioPaqueteDesde.Value > numericPrecioPaqueteHasta.Value)
            {
                hayError = true;
                errorMensaje += "Paquete: El precio mínimo debe ser menor a precio máximo;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        }

        public DataGridViewButtonColumn crearBotones(String nombreColumna, String leyendaBoton)
        {
            DataGridViewButtonColumn botones = new DataGridViewButtonColumn();
            botones.HeaderText = nombreColumna;
            botones.Text = leyendaBoton;
            botones.UseColumnTextForButtonValue = true;
            botones.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            return botones;
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarFiltros();

                using (SqlConnection conexion = this.obtenerConexion())
                {

                    using (SqlCommand comando = new SqlCommand())
                    {
                        conexion.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(comando);
                        DataTable busqueda = new DataTable();

                        int filtrarPorHabilitacion;
                        if (comboHabilitación.Text.Equals("No seleccionado"))
                            filtrarPorHabilitacion = 0;
                        else
                            filtrarPorHabilitacion = 1;

                        comando.Connection = conexion;
                        comando.CommandText = "SELECT * " +
                                              "FROM LOS_VIAJEROS_DEL_ANONIMATO.F_AplicarFiltrosRecorridos " +
                                              "(" + "'" + comboOrigen.Text + "'" +
                                              "," + "'" + comboDestino.Text + "'" +
                                              "," + "'" + comboServicio.Text + "'" +
                                              "," + filtrarPorHabilitacion.ToString() +
                                              "," + (Math.Max(comboHabilitación.SelectedIndex - 1, 0)).ToString() +
                                              "," + numericPrecioDesde.Value.ToString().Replace(',','.') +
                                              "," + numericPrecioHasta.Value.ToString().Replace(',', '.') +
                                              "," + numericPrecioPaqueteDesde.Value.ToString().Replace(',', '.') +
                                              "," + numericPrecioPaqueteHasta.Value.ToString().Replace(',', '.') + ")";
                        
                        adapter.Fill(busqueda);

                        dataGridView1.DataSource = busqueda;
                        dataGridView1.ReadOnly = true;
                        
                        dataGridView1.Columns[0].ReadOnly = false;
                        dataGridView1.Columns[1].ReadOnly = false;
                        dataGridView1.Columns[0].Visible = true;
                        dataGridView1.Columns[1].Visible = true;
                    }
                }
            }
            catch(ParametrosIncorrectosException ex)
            {
                (new Dialogo(ex.Message, "Aceptar")).Show();
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String origen = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                String destino = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                String servicio = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                if(e.ColumnIndex == 0)
                {
                    (new BajaRecorrido(origen,destino,servicio)).Show();
                }
                if (e.ColumnIndex == 1)
                {
                    (new ModifFormularioRecorrido(origen,destino,servicio)).Show();
                }

            }
        }

    }
}
