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
    public partial class AgregarEncomienda : Form1
    {
        private int codigoViaje { get; set; }
        public int DNI_Pasajero = -1;
        public String NombrePasajero = "";
        public String ApellidoPasajero = "";
        public decimal kilogramosPaquete = -1;
        
        public AgregarEncomienda(int codigoViajeHeredado)
        {
            InitializeComponent();
            this.codigoViaje = codigoViajeHeredado;
        }

        private void buttonEspecificarCliente_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertarCliente insercion = new InsertarCliente();
            (insercion).ShowDialog();
            textBoxDNI.Text = insercion.DNI_Cliente_Agregado.ToString();
            textBoxApellido.Text = insercion.Apellido_Cliente_Agregado;
            textBoxNombre.Text = insercion.Nombre_Cliente_Agregado;
            this.Show();
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

            if (numericPeso.Value == 0)
            {
                hayError = true;
                errorMensaje += "Falta completar peso del paquete;";
            }

            if (numericPeso.Value < 0)
            {
                hayError = true;
                errorMensaje += "El peso debe ser positivo;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarCampos();

                DNI_Pasajero = Convert.ToInt32(textBoxDNI.Text);
                NombrePasajero = textBoxNombre.Text;
                ApellidoPasajero = textBoxApellido.Text;
                kilogramosPaquete = numericPeso.Value;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ParametrosIncorrectosException ex)
            {
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
            }
        }

    }
}
