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
    public partial class AgregarEncomienda : Form1
    {
        private int codigoViaje { get; set; }
        private int numeroVoucher { get; set; }
        
        public AgregarEncomienda(int codigoViajeHeredado,int numVoucher)
        {
            InitializeComponent();
            this.numeroVoucher = numVoucher;
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

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.InsertarEncomienda", conexion))
                    {
                        conexion.Open();
                        comand.CommandType = CommandType.StoredProcedure;

                        comand.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
                        comand.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.numeroVoucher;
                        comand.Parameters.Add("@DNI_pasajero", SqlDbType.Int).Value = Convert.ToInt32(textBoxDNI.Text);
                        comand.Parameters.Add("@kilosPaqueteString", SqlDbType.VarChar).Value = numericPeso.Value.ToString().Replace(',', '.');
                        comand.Parameters.Add("@codigoEncomienda", SqlDbType.Int,18).Direction = ParameterDirection.Output;
                        comand.ExecuteNonQuery();

                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
