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
                textBoxCompania.Enabled = true;
                checkBoxCuotas.Enabled = true;
            }
            else
            {
                numericNumTarjeta.Enabled = false;
                textBoxCompania.Enabled = false;
                checkBoxCuotas.Enabled = false;
                numericNumTarjeta.Value = 0;
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
            textBoxCompania.Text = "";
            checkBoxCuotas.Checked = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
       
    }
}
