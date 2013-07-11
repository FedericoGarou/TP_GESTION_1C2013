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
    public partial class InsertarCliente : Form1
    {
        private DateTime fechaNacimiento = DateTime.MinValue;
        private Boolean registrarNuevo = false;
        public Boolean discapacidad = false;
        public int DNI_Cliente_Agregado;
        public String Apellido_Cliente_Agregado;
        public String Nombre_Cliente_Agregado;

        public InsertarCliente()
        {
            InitializeComponent();
        }

        private void ObtenerCliente()
        {
            using (SqlConnection conexion2 = this.obtenerConexion())
            {
                using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_Obtener_Usuario", conexion2))
                {
                    conexion2.Open();
                    comand.CommandType = CommandType.StoredProcedure;


                    comand.Parameters.Add("@DNI",SqlDbType.Int).Value = numericDNI.Value.ToString();
                    comand.Parameters.Add("@Nombre",SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Apellido", SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Direccion", SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Telefono", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Mail", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Fecha_Nac", SqlDbType.DateTime).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@Sexo", SqlDbType.VarChar,9).Direction = ParameterDirection.Output;
                    comand.Parameters.Add("@discapacidad", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    comand.ExecuteNonQuery();
                    
                    textBoxApellido.Text = comand.Parameters["@Apellido"].Value.ToString();
                    textBoxNombre.Text = comand.Parameters["@Nombre"].Value.ToString();
                    textBoxDireccion.Text = comand.Parameters["@Direccion"].Value.ToString();
                    numericTelefono.Value = Convert.ToInt32(comand.Parameters["@Telefono"].Value);
                    textBoxMail.Text = comand.Parameters["@Mail"].Value.ToString();
                    textBoxFechaNac.Text = Convert.ToDateTime(comand.Parameters["@Fecha_Nac"].Value.ToString()).ToShortDateString();
                    String masculino = comand.Parameters["@Sexo"].Value.ToString();
                    if (masculino.Equals("Masculino"))
                        radioMasculino.Checked = true;
                    else
                        radioBFemenino.Checked = true;

                    checkDiscapacidad.Checked = Convert.ToBoolean(comand.Parameters["@discapacidad"].Value);


                }
            }
        }

        private Boolean ExisteElUsuario()
        {
            Boolean existe = false;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_Existe_Usuario", conexion))
                {
                    conexion.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@DNI", SqlDbType.NVarChar).Value = numericDNI.Value;
                    command.Parameters.Add("@existe", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    existe = Convert.ToBoolean(command.Parameters["@existe"].Value);

                }
            }

            return existe;
        }

        //Elegir fecha de nacimiento
        private void buttonFechaNac_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textBoxFechaNac.Text = ex.Message;
                fechaNacimiento = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

        public void validarDatos()
        {
            Boolean hayError = false;
            String errorMensaje = "";

            if(textBoxApellido.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar apellido;";
            }

            if (textBoxNombre.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar nombre;";
            }

            if (textBoxDireccion.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar dirección;";
            }

            if (numericTelefono.Value == 0)
            {
                hayError = true;
                errorMensaje += "Falta completar telefono;";
            }

            if (textBoxMail.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar mail;";
            }

            if (textBoxFechaNac.Text.Equals(""))
            {
                hayError = true;
                errorMensaje += "Falta completar fecha de nacimiento;";
            }
            else
            {
                if (Convert.ToDateTime(textBoxFechaNac.Text) >= this.getFechaActual())
                {
                    hayError = true;
                    errorMensaje += "La fecha de nacimiento no puede ser posterior a la fecha del sistema;";
                }
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMensaje);

        }

        // Boton listo
        /* El usuario ingresa el DNI y se comprueba si es
         * un usuario del sistema
         */
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBoxApellido.Enabled = true;
            textBoxNombre.Enabled = true;
            textBoxDireccion.Enabled = true;
            numericTelefono.Enabled = true;
            textBoxMail.Enabled = true;
            buttonFechaNac.Enabled = true;
            radioBFemenino.Enabled = true;
            radioMasculino.Enabled = true;
            checkDiscapacidad.Enabled = true;
            buttonTerminado.Enabled = true;
            
            if (this.ExisteElUsuario())
                this.ObtenerCliente();
            else
            {
                this.Hide();
                DialogResult dr = (new DialogoInsertarCliente()).ShowDialog();

                if(dr == DialogResult.Cancel)
                    this.Show();
                if(dr == DialogResult.OK)
                {
                    this.Show();
                    this.Focus();
                    registrarNuevo = true;
                    textBoxApellido.Text = "";
                    textBoxNombre.Text = "";
                    textBoxDireccion.Text = "";
                    numericTelefono.Value = 0;
                    textBoxMail.Text = "";
                    textBoxFechaNac.Text = "";
                    radioBFemenino.Checked = false;
                    radioMasculino.Checked = false;
                    checkDiscapacidad.Checked = false;
                    buttonTerminado.Enabled = true;
                }
            }
            button1.Enabled = true;
        }

        private void buttonTerminado_Click(object sender, EventArgs e)
        {

            try
            {
                this.validarDatos();
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand())
                    {
                        conexion.Open();
                        comando.Connection = conexion;
                        
                        comando.Parameters.Add("@DNI",SqlDbType.Int).Value = numericDNI.Value;
	                    comando.Parameters.Add("@Nombre",SqlDbType.NVarChar,255).Value = textBoxNombre.Text;
	                    comando.Parameters.Add("@Apellido",SqlDbType.NVarChar,255).Value = textBoxApellido.Text;
                        comando.Parameters.Add("@Direccion", SqlDbType.NVarChar, 255).Value = textBoxDireccion.Text ;
	                    comando.Parameters.Add("@Telefono",SqlDbType.Int).Value = numericTelefono.Value;
	                    comando.Parameters.Add("@Mail",SqlDbType.NVarChar,255).Value = textBoxMail.Text;
                        comando.Parameters.Add("@Fecha_nac", SqlDbType.DateTime).Value = Convert.ToDateTime(textBoxFechaNac.Text);
                        
                        if(radioMasculino.Checked == true)
	                        comando.Parameters.Add("@Sexo",SqlDbType.VarChar,9).Value = "Masculino";
                        if(radioBFemenino.Checked == true)
                            comando.Parameters.Add("@Sexo", SqlDbType.VarChar, 9).Value = "Femenino";
                        
                        if (checkDiscapacidad.Checked == true)
                            comando.Parameters.Add("@discapacidad", SqlDbType.Bit).Value = 1;
                        else
                            comando.Parameters.Add("@discapacidad", SqlDbType.Bit).Value = 0;

                        comando.CommandType = CommandType.StoredProcedure;

                        if (registrarNuevo)
                            this.terminarRegistrando(comando); 
                        else
                            this.terminarActualizando(comando);

                        this.discapacidad = checkDiscapacidad.Checked;
                        this.DNI_Cliente_Agregado = Convert.ToInt32(numericDNI.Value);
                        this.Apellido_Cliente_Agregado = textBoxApellido.Text;
                        this.Nombre_Cliente_Agregado = textBoxNombre.Text;
                        

                    }

                }
                
                this.Close();
            }
            catch(ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
                this.Show();
                this.Focus();
            }
        }

        private void terminarActualizando(SqlCommand command)
        {
            command.CommandText = "LOS_VIAJEROS_DEL_ANONIMATO.SP_Actualizar_Cliente";
            command.ExecuteNonQuery();
        }

        private void terminarRegistrando(SqlCommand command)
        {
            command.CommandText = "LOS_VIAJEROS_DEL_ANONIMATO.SP_Insertar_Cliente";
            command.ExecuteNonQuery(); 
        }

        private void numericDNI_ValueChanged(object sender, EventArgs e)
        {
            textBoxApellido.Text = "";
            textBoxNombre.Text = "";
            textBoxDireccion.Text = "";
            numericTelefono.Value = 0;
            textBoxMail.Text = "";
            textBoxFechaNac.Text = "";
            radioBFemenino.Checked = false;
            radioMasculino.Checked = false;
            checkDiscapacidad.Checked = false;
        }


    }
}
