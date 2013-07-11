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
    public partial class AgregarPasaje : Form1
    {
        private int codigoViaje {get;set;}
        private int numeroVoucher;
        private bool discapacidad;
        private bool jubilado;
        private String query;
        
        public AgregarPasaje(int codigoViajeHeredado,int nroVoucherCompra,String tipo)
        {
            InitializeComponent();
            this.codigoViaje = codigoViajeHeredado;
            this.numeroVoucher = nroVoucherCompra;
            if(tipo.Equals("Por tutor"))
                this.query = "LOS_VIAJEROS_DEL_ANONIMATO.InsertarPasajeTutor";
            else
                this.query = "LOS_VIAJEROS_DEL_ANONIMATO.InsertarPasaje";
        }

        private void AgregarPasaje_Load(object sender, EventArgs e)
        {}

        // Insertar pasajero
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertarCliente insercion = new InsertarCliente();
            (insercion).ShowDialog();
            textBoxDNI.Text = insercion.DNI_Cliente_Agregado.ToString();
            textBoxApellido.Text = insercion.Apellido_Cliente_Agregado;
            textBoxNombre.Text = insercion.Nombre_Cliente_Agregado;
            this.discapacidad = insercion.discapacidad;
            //this.jubilado = insercion.jubilado;
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

            if (this.discapacidad)
            {
                if (this.HayOtroPasajeroConDiscapacidad())
                {
                    hayError = true;
                    errorMensaje += "Ya hay un pasajero con alguna discapacidad en esta compra;";
                }
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);    
        }

        private int obtenerCodigoButaca(int numero, int codViaje)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerCodigoButaca", conexion))
                {
                    conexion.Open();

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
                    comando.Parameters.Add("@numeroButaca", SqlDbType.Int).Value = numero;
                    comando.Parameters.Add("@codigoButaca", SqlDbType.Int).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    return Convert.ToInt32(comando.Parameters["@codigoButaca"].Value);

                }
            }
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarCampos();

                using (SqlConnection conexion = this.obtenerConexion())
                { 
                    using(SqlCommand comand = new SqlCommand(this.query,conexion))
                    {
                        conexion.Open();
                        comand.CommandType = CommandType.StoredProcedure;

                        comand.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
		                comand.Parameters.Add("@numeroVoucher",SqlDbType.Int).Value = this.numeroVoucher;
	                    comand.Parameters.Add("@DNI_pasajero",SqlDbType.Int).Value = Convert.ToInt32(textBoxDNI.Text);
                        comand.Parameters.Add("@codigoButaca", SqlDbType.Int).Value = this.obtenerCodigoButaca(Convert.ToInt32(textBoxNumeroB.Text), this.codigoViaje);

                        comand.ExecuteNonQuery();
                    }
                }

                if (this.discapacidad)
                {
                    this.Hide();
                    DialogoInsertarTutor dialogo = new DialogoInsertarTutor(this.numeroVoucher, this.codigoViaje, Convert.ToInt32(textBoxDNI.Text));
                    DialogResult dr = dialogo.ShowDialog();
                }

                this.Close();
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
            catch (SqlException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private Boolean HayOtroPasajeroConDiscapacidad()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.CantidadDiscapacitados", conexion))
                {
                    conexion.Open();
                    comand.CommandType = CommandType.StoredProcedure;

                    comand.Parameters.Add("@numeroVoucher",SqlDbType.Int).Value = this.numeroVoucher;
                    comand.Parameters.Add("@cantidadDiscapacitados", SqlDbType.Int).Direction = ParameterDirection.Output;

                    comand.ExecuteNonQuery();

                    int cantidadPasajerosConDiscapacidad = Convert.ToInt32(comand.Parameters["@cantidadDiscapacitados"].Value);

                    return cantidadPasajerosConDiscapacidad >= 1;
                        
                }
            }    
        }

        // Cancelar
        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

      

        
    }
}
