using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Rol
{
    public partial class BajaRol : Form1
    {
        string nombreRol;

        public BajaRol()
        {
            {
                
                InitializeComponent();
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    try
                    {
                        //cargar comboBox
                        conexion.Open();

                        SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol", conexion);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable tablaDeNombres = new DataTable();

                        adapter.Fill(tablaDeNombres);

                        comboBox1.DisplayMember = "Nombre_Rol";
                        comboBox1.DataSource = tablaDeNombres;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    nombreRol = comboBox1.Text;

                    SqlCommand inhabilitar = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Rol SET Habilitacion=0 WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int filasAfectadas = (int)inhabilitar.ExecuteNonQuery();

                    SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int codigoRol = (int)codRol.ExecuteScalar();

                    codRol.CommandText = "USE GD1C2013 DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol WHERE Codigo_Rol='" + codigoRol + "'";
                    codRol.ExecuteNonQuery();

                    new Dialogo(nombreRol + " inhabilitado \n", "Aceptar").ShowDialog();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }
    }
}
