using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class EspecificarPago : Form1
    {
        public int DNI_Abonante = -1;
        public String tipoPago = "";
        public int numeroTarjeta = -1;
        public String claveTarjeta = "";
        public String companiaTarjeta = "";
        
        public EspecificarPago()
        {
            InitializeComponent();
        }

        private void buttonEspecificarPagante_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertarCliente insercion = new InsertarCliente();
            (insercion).ShowDialog();
            textBoxDNI.Text = insercion.DNI_Cliente_Agregado.ToString();
            textBoxApellido.Text = insercion.Apellido_Cliente_Agregado;
            textBoxNombre.Text = insercion.Nombre_Cliente_Agregado;
            this.Show();
        }

        private void EspecificarPago_Load(object sender, EventArgs e)
        {
            comboBoxTipoPago.Items.AddRange(new String[] { "Tipo de pago", "Con tarjeta", "En efectivo" });
            comboBoxTipoPago.SelectedIndex = 0;
        }

        private void comboBoxTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipoPago.SelectedIndex == 1)
            {
                numericNumTarjeta.Enabled = true;
                textBoxClave.Enabled = true;
                buttonFechaVto.Enabled = true;
                textBoxCompania.Enabled = true;
                checkBoxCuotas.Enabled = true;
            }
            else
            {
                numericNumTarjeta.Enabled = false;
                textBoxClave.Enabled = false;
                buttonFechaVto.Enabled = false;
                textBoxCompania.Enabled = false;
                checkBoxCuotas.Enabled = false;
                numericNumTarjeta.Value = 0;
                textBoxFechaVto.Text = "";
                textBoxClave.Text = "";
                textBoxCompania.Text = "";
                checkBoxCuotas.Checked = false;
            }

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxDNI.Text = "";
            textBoxApellido.Text = "";
            textBoxNombre.Text = "";
            comboBoxTipoPago.SelectedIndex = 0;
            numericNumTarjeta.Value = 0;
            textBoxFechaVto.Text = "";
            textBoxClave.Text = "";
            textBoxCompania.Text = "";
            checkBoxCuotas.Checked = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void validarCampos()
        {
            Boolean hayError = false;
            String errorMensaje = "";

            if (textBoxDNI.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar DNI;";
            }

            if (textBoxApellido.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar apellido;";
            }

            if (textBoxNombre.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar nombre;";
            }

            if (comboBoxTipoPago.Text.Equals("Tipo de pago"))
            {
                hayError = true;
                errorMensaje += "Falta especificar un tipo de pago;";
            }

            if (comboBoxTipoPago.Text.Equals("Con tarjeta"))
            {
                if (textBoxCompania.Text.Equals(""))
                {
                    hayError = true;
                    errorMensaje += "Falta especificar un companía de tarjeta;";
                }

                if (numericNumTarjeta.Value == 0)
                {
                    hayError = true;
                    errorMensaje += "Falta especificar un numero de tarjeta;";
                }

                if (Convert.ToDateTime(textBoxFechaVto.Text) <= this.getFechaActual())
                {
                    hayError = true;
                    errorMensaje += "La tarjeta esta vencida;";
                }

            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarCampos();

                DNI_Abonante = Convert.ToInt32(textBoxDNI.Text);
                tipoPago = comboBoxTipoPago.Text;
                numeroTarjeta = Convert.ToInt32(numericNumTarjeta.Value);
                companiaTarjeta = textBoxCompania.Text;
                claveTarjeta = textBoxClave.Text;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ParametrosIncorrectosException ex)
            {
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
            }
        }

        private void buttonFechaVto_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textBoxFechaVto.Text = ex.Message;
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

    }
}
