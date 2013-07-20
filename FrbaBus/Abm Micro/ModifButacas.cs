using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Micro
{
    public partial class ModifButacas : Form1
    {
        public ModifButacas(string unaPatente)
        {
            
            
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {

                label3.Text = unaPatente;
                conexion.Open();
                SqlCommand butacas = new SqlCommand("USE GD1C2013 select Cantidad_Butacas from LOS_VIAJEROS_DEL_ANONIMATO.MICRO where Patente = '" + unaPatente + "'", conexion);
                string butacasTotales = butacas.ExecuteScalar().ToString();

                textBox1.Text = butacasTotales;
                textBox1.Enabled = false;

                SqlCommand kgs = new SqlCommand("USE GD1C2013 select KG_Disponibles from LOS_VIAJEROS_DEL_ANONIMATO.MICRO where Patente = '" + unaPatente + "'", conexion);
                string kgsTotales = kgs.ExecuteScalar().ToString();

                textBox2.Text = kgsTotales;

                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 select NumeroButaca, Ubicacion, Piso from LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO WHERE Patente = '" + unaPatente + "' order by 1", ref tabla, conexion);

                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;

                DataGridViewButtonColumn botonEliminar = this.crearBotones("", "Eliminar Butaca");
                dataGridView1.Columns.Add(botonEliminar);             



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string patente = label3.Text; 
            string kgs = textBox2.Text;
            
            try
            {
                if(!(Convert.ToInt32(kgs)>0)){
                throw new Exception ("Kgs ingresados deben ser mayores a 0");
                }

                
                using (SqlConnection conexion = this.obtenerConexion())
                {                
                    conexion.Open();
                    SqlCommand actializarKgs = new SqlCommand("USE GD1C2013 UPDATE LOS_VIAJEROS_DEL_ANONIMATO.MICRO SET KG_Disponibles = " + kgs + " where Patente = '" + patente + "'", conexion);
                    actializarKgs.ExecuteNonQuery();
                    (new Dialogo("Capacidad maxima de kgs modificada;Nueva capacidad: "+kgs, "Aceptar")).ShowDialog();

        
                }
            } catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string patente = label3.Text;
            (new AgregarButaca(patente)).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string unaPatente = label3.Text;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                dataGridView1.Columns.Clear();
                conexion.Open();
                SqlCommand butacas = new SqlCommand("USE GD1C2013 select Cantidad_Butacas from LOS_VIAJEROS_DEL_ANONIMATO.MICRO where Patente = '" + unaPatente + "'", conexion);
                string butacasTotales = butacas.ExecuteScalar().ToString();

                textBox1.Text = butacasTotales;
                textBox1.Enabled = false;

                SqlCommand kgs = new SqlCommand("USE GD1C2013 select KG_Disponibles from LOS_VIAJEROS_DEL_ANONIMATO.MICRO where Patente = '" + unaPatente + "'", conexion);
                string kgsTotales = kgs.ExecuteScalar().ToString();

                textBox2.Text = kgsTotales;

                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 select NumeroButaca, Ubicacion, Piso from LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO WHERE Patente = '" + unaPatente + "' order by 1", ref tabla, conexion);

                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;

                DataGridViewButtonColumn botonEliminar = this.crearBotones("", "Eliminar Butaca");
                dataGridView1.Columns.Add(botonEliminar);
            }
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string patente = label3.Text; 
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    
                    String numeroButaca = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        conexion.Open();
                        if (e.ColumnIndex == 3)
                        {
                            SqlCommand borrarButaca = new SqlCommand("USE GD1C2013 DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO WHERE Patente='"+patente+" and NumeroButaca='"+numeroButaca+"'", conexion);
                            borrarButaca.ExecuteNonQuery();
                            (new Dialogo("Butaca "+numeroButaca+" borrada", "Aceptar")).ShowDialog();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - La butaca esta siendo utilizada;No se puede borrar", "Aceptar")).ShowDialog();
            }
                        
        }
        


    }
}
