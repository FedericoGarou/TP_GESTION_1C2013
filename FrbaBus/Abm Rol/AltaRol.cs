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
    public partial class AltaRol : Form1
    {
        string nombreRol;
        string funcionalidad;

        public AltaRol()
        {
            
                InitializeComponent();
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    try
                    {
                        //cargar comboBox
                        conexion.Open();
                                                
                        SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad", conexion);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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


        //agregar rol
        protected virtual void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();

                    nombreRol = textBox1.Text;

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);

                    int cantidadDeFilas = (int)cmd.ExecuteScalar();

                    if (cantidadDeFilas != 0)
                    {
                        (new Dialogo("Ya existe el rol", "Aceptar")).ShowDialog();                        
                    }
                    else
                    {
                        cmd.CommandText = "USE GD1C2013 INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol VALUES ('" + nombreRol + "', 1)";
                        cmd.ExecuteNonQuery();
                        SqlCommand codRol = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Rol.Nombre_Rol = '" + nombreRol + "'", conexion);
                        int codigoRol = (int)codRol.ExecuteScalar();
                        
                        foreach (String nombreFunc in listBox1.Items)
                        {
                            SqlCommand codFunc = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad WHERE Funcionalidad.Nombre_Funcionalidad = '" + nombreFunc + "'", conexion);
                            int codigoFuncionalidad = (int)codFunc.ExecuteScalar();
                            
                            cmd.CommandText = "USE GD1C2013 INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad VALUES (" + codigoRol + "," + codigoFuncionalidad + ")";
                            cmd.ExecuteNonQuery();
                        }

                        int filasAfectadasTotales = 1 + listBox1.Items.Count;
                        new Dialogo(nombreRol + " agregado \n" + filasAfectadasTotales + " filas afectadas", "Aceptar").ShowDialog();
                        
                    }
                }
        

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }

        }

        //boton agregar     
        private void button2_Click(object sender, EventArgs e) 
        {            
            funcionalidad = comboBox1.Text;

            if (listBox1.Items.Contains(funcionalidad))
            {
                (new Dialogo("Ya existe la funcionalidad", "Aceptar")).ShowDialog();
                
            }
            else
            {
                listBox1.Items.Add(funcionalidad);
                comboBox1.Text = "";
            }                  
        }

        
        //boton borrar
        private void button3_Click(object sender, EventArgs e) 
        {

            funcionalidad = comboBox1.Text;

           
            if (listBox1.Items.Contains(funcionalidad))
            {
                listBox1.Items.Remove(funcionalidad);

                comboBox1.Text = ""; 

            }
            else
            {
                (new Dialogo("No existe la funcionalidad", "Aceptar")).ShowDialog();
            }     
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            listBox1.Items.Clear();
        }
    
    
    }
}
