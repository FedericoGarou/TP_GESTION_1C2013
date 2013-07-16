using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace FrbaBus.Cancelar_Viaje
{
    public partial class CancelarPasajeEncomienda : Form1
    {
        DateTime fechaSalida;

        public CancelarPasajeEncomienda()
        {
            InitializeComponent();

            textBoxFecha.Text = "No seleccionado";

            fechaSalida = DateTime.MinValue;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();
                using (SqlCommand cmdParaLlenarComboBox = new SqlCommand("USE GD1C2013 SELECT NombreCiudad FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Ciudades () ORDER BY RN", conexion))
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
            comboVouchers.SelectedIndex = 0;
            comboVouchers.Enabled = false;
            buttonSeleccionar.Enabled = false;
        }

        public void validarParametros()
        {
            Boolean hayError = false;
            String errorMensaje = "";

            if (comboOrigen.Text.Equals(comboDestino.Text) && !comboDestino.Text.Equals("No seleccionado"))
            {
                hayError = true;
                errorMensaje += "Origen y destino son la misma ciudad;";
            }

            if (comboDestino.SelectedIndex == 0 ||
                comboOrigen.SelectedIndex == 0 ||
                textBoxFecha.Text.Equals("No seleccionado"))
            {
                hayError = true;
                errorMensaje += "Debe especificar todos los parametros de busqueda;";
            }

            if (fechaSalida < this.getFechaActual() && fechaSalida != DateTime.MinValue)
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

                String query = "USE GD1C2013 SELECT NumeroVoucher FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Voucher ('" +
                                        comboOrigen.Text + "','" +
                                        comboDestino.Text + "','" +
                                        fechaFormateada + "') ORDER BY RN";

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    conexion.Open();
                    using (SqlCommand cmdParaLlenarComboBox = new SqlCommand(query, conexion))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmdParaLlenarComboBox);

                        // Llenar los combo box 'origen' y 'destino'

                        DataTable vouchers = new DataTable();
                        adapter.Fill(vouchers);
                        comboVouchers.DisplayMember = "NumeroVoucher";
                        comboVouchers.DataSource = vouchers;

                    }
                }

                comboVouchers.Enabled = true;
                buttonSeleccionar.Enabled = true;
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
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

        private void comboVouchers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVouchers.Text.Equals("-1"))
            {
                comboVouchers.Text = "No seleccionado";
            }
        }

        private void buttonSeleccionar_Click(object sender, EventArgs e)
        {
            if (comboVouchers.SelectedIndex == 0)
            {
                this.Hide();
                (new Dialogo("No se selecciono ningún voucher", "Aceptar")).ShowDialog();
                this.Show();
            }
            else
            {
                System.Data.DataRowView fila = (System.Data.DataRowView)comboVouchers.SelectedItem;
                this.Hide();
                (new EspecificarCancelacion(Convert.ToInt32(fila["NumeroVoucher"]))).ShowDialog();
                this.Close();
            }

        }
    }
}
