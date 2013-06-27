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
    public partial class ListadoRol : Form1
    {
        public ListadoRol()
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

                    comboBox1.Text = "";

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        //boton buscar
        private void button2_Click(object sender, EventArgs e)
        {
            string varFiltro1;
            string varFiltro2;
            string varFiltro3;

            varFiltro1 = textBox1.Text;
            varFiltro2 = textBox2.Text;
            varFiltro3 = comboBox1.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    DataTable tabla = new DataTable();

                    if (textBox1.Text.Length > 0)
                    {
                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol LIKE '%" + varFiltro1 + "%'", ref tabla, conexion);

                        if ((textBox2.Text.Length) > 0 && (varFiltro2 != varFiltro3))
                        {
                            cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + varFiltro2 + "' and Nombre_Rol NOT LIKE '%" + varFiltro1 + "%'", ref tabla, conexion);
                        }

                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + varFiltro3 + "' and Nombre_Rol NOT LIKE '%" + varFiltro1 + "%'", ref tabla, conexion);

                        dataGridView1.DataSource = tabla;

                    }
                    else
                    {

                        if ((textBox2.Text.Length) > 0 && (varFiltro2 != varFiltro3))
                        {
                            cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + varFiltro2 + "'", ref tabla, conexion);
                        }

                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + varFiltro3 + "'", ref tabla, conexion);

                        dataGridView1.DataSource = tabla;
                    }
                }

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        //boton limpiar
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            dataGridView1.DataSource = "";
        }
    }

}