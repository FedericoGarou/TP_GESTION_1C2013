using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaBus.Abm_Rol;
using FrbaBus.Abm_Recorrido;

namespace FrbaBus.Login
{
    public partial class Pantalla_Inicial : Form1
    {
        public Pantalla_Inicial()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    //cargar comboBox de cliente (por defecto)
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on ( rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) WHERE r.Nombre_Rol = 'Cliente'", conexion);

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

        private void button2_Click(object sender, EventArgs e)
        {
            (new Login()).Show();
        }

        
        public Pantalla_Inicial(string unUsuario)
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    //cargar comboBox de un usuario determinado
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario l join LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol ur on (l.DNI_Usuario = ur.DNI) join LOS_VIAJEROS_DEL_ANONIMATO.Rol r on (ur.Codigo_Rol = r.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on ( rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) WHERE Username='" + unUsuario + "'", conexion);

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

        private void button1_Click(object sender, EventArgs e)
        {
            string funcionalidad = comboBox1.Text;
            
            if (String.Equals(funcionalidad, "ABM Rol"))
            {
                (new ABM_Rol()).Show();
            }

            if (String.Equals(funcionalidad, "ABM Recorrido"))
            {
                (new ABM_Recorrido()).Show();
            }
        }
    }
}