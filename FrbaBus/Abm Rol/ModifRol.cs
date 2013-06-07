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
    public partial class ModifRol : Form
    {
        string nombreRol;

        public ModifRol()
        {
            InitializeComponent();
            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLSERVER2008;Database=GD1C2013;User Id=gd;Password=gd2013;"))
            {
                //cargar comboBox rol
                try
                {

                    conexion.Open();

                    SqlCommand rol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(rol);
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


        private void button5_Click(object sender, EventArgs e)
        {
            nombreRol = comboBox1.Text;
            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLSERVER2008;Database=GD1C2013;User Id=gd;Password=gd2013;"))
            {
                //cargar comboBox funcionalidades que tiene el rol
                try
                {
                    conexion.Open();

                    SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int codigoRol = (int)codRol.ExecuteScalar();

                    //  new Dialogo(codigoRol + "asd", "Aceptar").ShowDialog();


                    SqlCommand funcRol = new SqlCommand("USE GD1C2013 SELECT Nombre_Funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad r JOIN LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (r.Codigo_Funcionalidad = f.Codigo_Funcionalidad)WHERE r.Codigo_Rol=" + codigoRol, conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(funcRol);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox1.DisplayMember = "Nombre_Funcionalidad";
                    comboBox1.DataSource = tablaDeNombres;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }


        }


        //boton modificar rol
        private void button1_Click(object sender, EventArgs e)
        {
            nombreRol = comboBox1.Text;
            string nuevoNombreRol = textBox1.Text;
            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLSERVER2008;Database=GD1C2013;User Id=gd;Password=gd2013;"))
            {                
                try
                {
                    
                    conexion.Open();
                    SqlCommand modRol = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Rol SET Nombre_Rol='" + nuevoNombreRol + "' where Nombre_Rol='" + nombreRol + "'",conexion);                   
                    modRol.ExecuteNonQuery();
                    comboBox1.Text = nuevoNombreRol;
                    new Dialogo("Nombre de " + nombreRol + " modificado \n 1 fila afectada", "Aceptar").ShowDialog();
                                     
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