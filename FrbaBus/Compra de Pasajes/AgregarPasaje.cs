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
    public partial class AgregarPasaje : Form1
    {
        private int codigoViaje {get;set;}
        public int DNI_Pasajero = -1;
        public String NombrePasajero = "";
        public String ApellidoPasajero = "";
        public int numeroButaca = -1;
        public int pisoButaca = -1;
        public String ubicacionButaca = "";

        public AgregarPasaje(int codigoViajeHeredado)
        {
            InitializeComponent();
            this.codigoViaje = codigoViajeHeredado;   
        }

        private void AgregarPasaje_Load(object sender, EventArgs e)
        {}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertarCliente insercion = new InsertarCliente();
            (insercion).ShowDialog();
            textBoxDNI.Text = insercion.DNI_Cliente_Agregado.ToString();
            textBoxApellido.Text = insercion.Apellido_Cliente_Agregado;
            textBoxNombre.Text = insercion.Nombre_Cliente_Agregado;
            this.Show();
        }

        private void buttonEspecificarB_Click(object sender, EventArgs e)
        {
            this.Hide();
            MostrarButacasDisponibles insercionB = new MostrarButacasDisponibles(this.codigoViaje);
            DialogResult dr = insercionB.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBoxNumeroB.Text = insercionB.numeroBElegido.ToString();
                textBoxPisoB.Text = insercionB.pisoElegido.ToString();
                textBoxUbiB.Text = insercionB.ubicacionElegida.ToString();
            }
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

            if (textBoxPisoB.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar piso de butaca;";
            }

            if (textBoxNumeroB.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar numero de butaca;";
            }

            if (textBoxUbiB.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar ubicación de butaca;";
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
                numeroButaca = Convert.ToInt32(textBoxNumeroB.Text);
                pisoButaca = Convert.ToInt32(textBoxPisoB.Text);
                ubicacionButaca = textBoxUbiB.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ParametrosIncorrectosException ex)
            {
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

      

        
    }
}
