using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Canje_de_Ptos
{
    public partial class CanjePuntos : Form1
    {
        public CanjePuntos()
        {
            InitializeComponent();
            textBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dni = textBox1.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    if (!(Convert.ToInt64(textBox1.Text) > 0))
                    {
                        throw new Exception("DNI invalido;");
                    }

                    conexion.Open();
                    DataTable tabla = new DataTable();                   

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT SUM(Puntos) FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + dni, conexion);
                    string totalPuntos = cmd.ExecuteScalar().ToString();

                    textBox2.Text = totalPuntos;

                    cargarATablaParaDataGripView("select DetalleProducto, PuntosNecesarios from LOS_VIAJEROS_DEL_ANONIMATO.PREMIO where PuntosNecesarios<" + totalPuntos, ref tabla, conexion);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;

                    DataGridViewColumn cantidad = new DataGridViewTextBoxColumn();
                    cantidad.HeaderText = "Cantidad";
                    cantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns.Add(cantidad);
                    
                    DataGridViewButtonColumn botonCanjear = this.crearBotones("", "Canjear");
                    dataGridView1.Columns.Add(botonCanjear);
                    
                    
                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             try
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    String premio = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    int puntos = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    int cantidad = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    int puntosCliente = Convert.ToInt32(textBox2.Text);

                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        conexion.Open();

                        if (e.ColumnIndex == 3) //boton canjear
                        {
                            using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_CanjearPremio", conexion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@DNI", SqlDbType.NVarChar).Value = textBox1.Text;
                                cmd.Parameters.Add("@Premio", SqlDbType.NVarChar).Value = premio;
                                cmd.Parameters.Add("@Puntos", SqlDbType.Int).Value = puntos;
                                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;                                
                                cmd.Parameters.Add("@PuntosCliente", SqlDbType.Int).Value = puntosCliente;
                                cmd.Parameters.Add("@FechaActual", SqlDbType.DateTime).Value = getFechaActual();
                                cmd.Parameters.Add("@retorno", SqlDbType.Bit).Direction = ParameterDirection.Output;

                                cmd.ExecuteNonQuery();

                                int seRealizoCanje = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                                if (seRealizoCanje == 1)
                                {
                                    SqlCommand cmd2 = new SqlCommand("USE GD1C2013 SELECT SUM(Puntos) FROM LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF WHERE DNI_Usuario = " + textBox1.Text, conexion);
                                    string totalPuntos = cmd2.ExecuteScalar().ToString();

                                    textBox2.Text = totalPuntos;
                                    
                                    (new Dialogo("Canje realizado con exito;Premio: " +premio+";Cantidad: "+cantidad+ ";Puntos gastados: "+(puntos*cantidad), "Aceptar")).ShowDialog();
                                }

                                if (seRealizoCanje == 0)
                                {
                                    (new Dialogo("Canje no realizado;Puntos insuficientes para la cantidad seleccionada", "Aceptar")).ShowDialog();
                                }
                            }
                        }
                    }
                }

             }catch (Exception ex)
             {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
             }

        }
    }
}
