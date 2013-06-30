using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;



namespace FrbaBus.Login
{
    public partial class Login : Form1
    {
        public Login()
        {
            InitializeComponent();

        }

        //boton inciar sesion
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    string usuario = textBox1.Text;
                    string pass = SHA256Encrypt(textBox2.Text);
                                                          
                    //SqlCommand IntentosFallidos = new SqlCommand("USE GD1C2013 SELECT Intentos_Fallidos FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = '" + usuario + "' and Passwd = '" + pass + "'", conexion);
                    //int cantidadIntentosFallidos = (int) IntentosFallidos.ExecuteScalar();

                    //new Dialogo(usuario + pass , "Aceptar").ShowDialog();

                    //if (cantidadIntentosFallidos < 3)


                    // TODO OBTENER INTENTOS FALLIDOS Y COMPARAR EN EL IF
                    if (0 < 3)
                    {
                                                
                        SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = '" + usuario + "' and Passwd = '" + pass + "'", conexion);
                        int cantidadDeFilas = (int)cmd.ExecuteScalar();

                        if (cantidadDeFilas == 1)
                        {
                            new Pantalla_Inicial(usuario).Show();
                        }
                        else
                        {
                            SqlCommand aumentarIntentoFallido = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario SET Intentos_Fallidos=(Intentos_Fallidos+1) WHERE Username='" + usuario + "'", conexion);
                            aumentarIntentoFallido.ExecuteNonQuery();
                            new Dialogo("Login incorrecto, vuelva a intentarlo", "Aceptar").ShowDialog();
                        }

                    }
                    else
                    {
                        new Dialogo("Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos", "Aceptar").ShowDialog();
                    }


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        public string SHA256Encrypt(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        } 
    }
}

     

