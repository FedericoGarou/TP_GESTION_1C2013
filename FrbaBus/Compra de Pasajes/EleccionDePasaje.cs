using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class EleccionDePasaje : Form1
    {
        DateTime fechaSalida;

        public EleccionDePasaje()
        {
            InitializeComponent();

            DataGridViewButtonColumn botonesComprar = this.crearBotones("Comprar", "Comprar");
            dataGridView1.Columns.Add(botonesComprar);
            dataGridView1.Columns[0].Visible = false;

            textBoxFecha.Text = "No seleccionado";

            fechaSalida = DateTime.MinValue;
                
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                using( SqlCommand cmdParaLlenarComboBox = new SqlCommand("USE GD1C2013 SELECT NombreCiudad FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Ciudades () ORDER BY RN",conexion) )
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdParaLlenarComboBox);
    
                    // Llenar los combo box 'origen' y 'destino'
    
                    DataTable ciudadesOrigen = new DataTable();
                    adapter.Fill(ciudadesOrigen);
                    comboOrigen.DisplayMember = "NombreCiudad";
                    comboOrigen.DataSource = ciudadesOrigen;
    
                    DataTable ciudadesDestino = new DataTable();
                    adapter.Fill(ciudadesDestino);
                    comboDestino.DisplayMember = "NombreCiudad";
                    comboDestino.DataSource = ciudadesDestino;
                }

                
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            comboOrigen.SelectedIndex = 0;
            comboDestino.SelectedIndex = 0;
            textBoxFecha.Text = "No seleccionado";
            fechaSalida = DateTime.MinValue;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.DataSource = null;
        }

        private void buttonFecha_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textBoxFecha.Text = ex.Message;
                fechaSalida = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }
    
        // Validar que los parametros sean correctos.
        public void validarParametros()
        {
            Boolean hayError = false;
            String errorMensaje = "";

            if (comboOrigen.Text.Equals(comboDestino.Text) && !comboDestino.Text.Equals("No seleccionado"))
            {
                hayError = true;
                errorMensaje += "Origen y destino son la misma ciudad;";
            }

            if( comboDestino.SelectedIndex == 0 ||
                comboOrigen.SelectedIndex == 0 ||
                textBoxFecha.Text.Equals("No seleccionado"))
            {
                hayError = true;
                errorMensaje += "Debe especificar todos los parametros de busqueda;";
            }

            if( fechaSalida < this.getFechaActual() && fechaSalida != DateTime.MinValue )
            {
                hayError = true;
                errorMensaje += "La fecha debe ser mayor al día de hoy;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarParametros();

                String fechaFormateada = fechaSalida.Date.ToString("yyyyMMdd");
                
                String ConsultaDeBusqueda = "SELECT * " +
                                            "FROM LOS_VIAJEROS_DEL_ANONIMATO.F_TraerViajesParaComprar ( '" +
                                             comboOrigen.Text + "','" +
                                             comboDestino.Text + "','" +
                                             fechaFormateada + "')";

                dataGridView1.Columns[0].Visible = true;
                dataGridView1.ReadOnly = true;
                DataTable viajesCoincidentes = new DataTable();
                this.cargarATablaParaDataGripView(ConsultaDeBusqueda,ref viajesCoincidentes,this.obtenerConexion());
                dataGridView1.DataSource = viajesCoincidentes;
                dataGridView1.Columns[0].ReadOnly = false;
                             
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                this.Hide();
                int codigoViajeSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CodigoViaje"].Value);
                (new Dialogo("Vas a comprar un pasaje en el viaje "+codigoViajeSeleccionado, "Aceptar")).ShowDialog();
                (new EspecificarCompra(codigoViajeSeleccionado)).ShowDialog();
                this.Show();

            }
        }
    }
}
