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
    public partial class ModifRol : Form1
    {
        string nombreRol;
        string nombreFunc;

        public ModifRol()
        {
            InitializeComponent();
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            using (SqlConnection conexion = this.obtenerConexion())
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

        public ModifRol(String nombreRolAModificar)
        {
            InitializeComponent();

            comboBox1.Text = nombreRolAModificar;

            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        public ModifRol(String nombreRolAModificar, String nombreFuncionalidadAModificar)
        {
            InitializeComponent();

            comboBox1.Text = nombreRolAModificar;

            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;

            comboBox2.Text = nombreFuncionalidadAModificar;

            comboBox2.Enabled = false;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                //cargar comboBox3 todas las funcionalidades
                try
                {

                    conexion.Open();

                    SqlCommand funcs = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(funcs);
                    DataTable tablaDeFuncionalidades = new DataTable();

                    adapter.Fill(tablaDeFuncionalidades);

                    comboBox3.DisplayMember = "Nombre_Funcionalidad";
                    comboBox3.DataSource = tablaDeFuncionalidades;
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
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            nombreRol = comboBox1.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {

                try
                {
                    conexion.Open();

                    SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int codigoRol = (int)codRol.ExecuteScalar();

                    //  new Dialogo(codigoRol + "asd", "Aceptar").ShowDialog();


                    SqlCommand funcRol = new SqlCommand("USE GD1C2013 SELECT Nombre_Funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad r JOIN LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (r.Codigo_Funcionalidad = f.Codigo_Funcionalidad)WHERE r.Codigo_Rol=" + codigoRol, conexion);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(funcRol);
                    DataTable tablaDeNombres1 = new DataTable();

                    adapter1.Fill(tablaDeNombres1);

                    comboBox2.DisplayMember = "Nombre_Funcionalidad";
                    comboBox2.DataSource = tablaDeNombres1;


                    SqlCommand funcs = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad", conexion);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(funcs);
                    DataTable tablaDeNombres2 = new DataTable();

                    adapter2.Fill(tablaDeNombres2);

                    comboBox3.DisplayMember = "Nombre_Funcionalidad";
                    comboBox3.DataSource = tablaDeNombres2;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }


        }


        //boton modificar nombre rol
        private void button1_Click(object sender, EventArgs e)
        {
            nombreRol = comboBox1.Text;
            string nuevoNombreRol = textBox1.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {

                    conexion.Open();
                    SqlCommand modRol = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Rol SET Nombre_Rol='" + nuevoNombreRol + "' where Nombre_Rol='" + nombreRol + "'", conexion);
                    modRol.ExecuteNonQuery();
                    comboBox1.Text = nuevoNombreRol;
                    new Dialogo("Rol " + nombreRol + " modificado a " + nuevoNombreRol + "\n 1 fila afectada", "Aceptar").ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        //boton modificar nombre func
        private void button2_Click(object sender, EventArgs e)
        {
            nombreFunc = comboBox2.Text;
            string nuevoNombreFunc = textBox2.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {

                    conexion.Open();
                    SqlCommand modFunc = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad SET Nombre_Funcionalidad='" + nuevoNombreFunc + "' where Nombre_Funcionalidad='" + nombreFunc + "'", conexion);
                    modFunc.ExecuteNonQuery();
                    comboBox2.Text = nuevoNombreFunc;
                    new Dialogo("Funcionalidad " + nombreFunc + " modificada a " + nuevoNombreFunc + "\n 1 fila afectada", "Aceptar").ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }


        //boton elimimar func
        private void button3_Click(object sender, EventArgs e)
        {
            nombreRol = comboBox1.Text;
            nombreFunc = comboBox2.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {

                    conexion.Open();

                    SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int codigoRol = (int)codRol.ExecuteScalar();

                    SqlCommand codFunc = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad WHERE Nombre_Funcionalidad = '" + nombreFunc + "'", conexion);
                    int codigoFunc = (int)codFunc.ExecuteScalar();

                    SqlCommand delRolFunc = new SqlCommand("USE GD1C2013 DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad WHERE Codigo_Rol=" + codigoRol + "and Codigo_Funcionalidad=" + codigoFunc, conexion);
                    delRolFunc.ExecuteNonQuery();

                    comboBox2.Update();

                    new Dialogo("La funcionalidad " + nombreFunc + ", fue eliminada de " + nombreRol + "\n 1 fila afectada", "Aceptar").ShowDialog();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }


        //boton agregar func
        private void button4_Click(object sender, EventArgs e)
        {

            nombreRol = comboBox1.Text;
            nombreFunc = comboBox3.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                    int codigoRol = (int)codRol.ExecuteScalar();

                    SqlCommand codFunc = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad WHERE Nombre_Funcionalidad = '" + nombreFunc + "'", conexion);
                    int codigoFunc = (int)codFunc.ExecuteScalar();

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad WHERE Codigo_Rol =" + codigoRol + "and Codigo_Funcionalidad=" + codigoFunc, conexion);

                    int cantidadDeFilas = (int)cmd.ExecuteScalar();

                    if (cantidadDeFilas != 0)
                    {
                        (new Dialogo("El rol " + nombreRol + " ya posee la funcionalidad " + nombreFunc, "Aceptar")).ShowDialog();
                    }
                    else
                    {
                        SqlCommand addRolFunc = new SqlCommand("USE GD1C2013 INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad VALUES (" + codigoRol + "," + codigoFunc + ")", conexion);
                        addRolFunc.ExecuteNonQuery();
                        new Dialogo("La funcionalidad " + nombreFunc + ", fue agregada a " + nombreRol + "\n 1 fila afectada", "Aceptar").ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox3.Text = "";

            comboBox2.Enabled = false;
            comboBox3.Enabled = false;

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            nombreRol = comboBox1.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    SqlCommand validacion = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol where Nombre_Rol = '" + nombreRol + "' and Habilitacion=0", conexion);
                    int cantidadDeFilas = (int)validacion.ExecuteScalar();

                    if (cantidadDeFilas != 0)
                    {
                        SqlCommand habilitarRol = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Rol SET Habilitacion=1 where Nombre_Rol = '" + nombreRol + "'", conexion);
                        habilitarRol.ExecuteNonQuery();
                        (new Dialogo("El rol se ha habilitado", "Aceptar")).ShowDialog();                                                             
                    }

                    else
                    {                        
                        (new Dialogo("El rol ya esta habilitado", "Aceptar")).ShowDialog(); 
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("El rol ya esta habilitado", "Aceptar")).ShowDialog();
                }

            }



        }
    }
}