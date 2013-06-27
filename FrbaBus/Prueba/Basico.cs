using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Prueba
{
    public partial class Basico : Form1
    {
        public Basico()
        {
            InitializeComponent();

            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO", conexion);

                SqlDataAdapter adap = new SqlDataAdapter(command);

                DataTable tablaTipos = new DataTable();

                adap.Fill(tablaTipos);

                comboBox1.DisplayMember = "NombreServicio";
                comboBox1.DataSource = tablaTipos;

            }

        }

        private void Basico_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                conexion.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD", conexion);

                SqlDataAdapter adap = new SqlDataAdapter(command);

                DataTable tablaCiudad = new DataTable();

                adap.Fill(tablaCiudad);

                dataGridView1.DataSource = tablaCiudad;

            }
        }

    }
}
