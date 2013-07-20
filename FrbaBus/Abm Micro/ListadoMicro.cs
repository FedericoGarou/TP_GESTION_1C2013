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
    public partial class ListadoMicro : Form1
    {
        private int bodegaKGDesde;
        private int bodegaKGHasta;
        private DateTime fechaDesde;
        private DateTime fechaHasta;

        public ListadoMicro()
        {
            InitializeComponent();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textPatente.Text = "";
            comboMarca.SelectedIndex = 0;
            textModelo.Text = "";
            comboServicio.SelectedIndex = 0;
            numericCantBut.Value = 0;
            dataGridView1.DataSource = null;
        }

        private void buttonFinal_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand())
                    {

                        String fechaStDesde = this.fechaDesde.ToString("yyyyMMdd");
                        String fechaStHasta = this.fechaHasta.ToString("yyyyMMdd");

                        if (this.fechaDesde == DateTime.MinValue)
                            fechaStDesde = "20120101";
                        if (this.fechaHasta == DateTime.MinValue)
                            fechaStHasta = "20120101";

                        String patente = textPatente.Text;
                        if (textPatente.Text.Equals(""))
                            patente = "Ninguna";

                        String marca = comboMarca.Text;
                        if (comboMarca.SelectedIndex == 0)
                            marca = "Ninguna";

                        String modelo = textModelo.Text;
                        if (textModelo.Text.Equals(""))
                            modelo = "Ninguna";

                        String servicio = comboServicio.Text;
                        if (comboServicio.SelectedIndex == 0)
                            servicio = "Ninguna";

                        String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.AplicarFiltrosMicro(" +
                                    "'" + patente + "'," +
                                    "'" + marca + "'," +
                                    "'" + modelo + "'," +
                                    "'" + servicio + "'," +
                                    "'" + numericCantBut.Value.ToString() + "'," +
                                    "'" + fechaStDesde + "'," +
                                    "'" + fechaStHasta + "'," +
                                    this.bodegaKGDesde.ToString() + "," +
                                    this.bodegaKGHasta.ToString() + ")";

                        comando.Connection = conexion;
                        comando.CommandText = query;

                        conexion.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(comando);
                        DataTable filtrados = new DataTable();
                        adapter.Fill(filtrados);
                        dataGridView1.DataSource = filtrados;

                    }
                }
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
            catch (SqlException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void ListadoMicro_Load(object sender, EventArgs e)
        {
            String query = "";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand())
                {
                    conexion.Open();
                    comando.Connection = conexion;
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);

                    // Llenar combo marca
                    query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Marcas() ORDER BY RN ASC";
                    comando.CommandText = query;
                    DataTable marcas = new DataTable();
                    adapter.Fill(marcas);
                    comboMarca.DisplayMember = "Marca";
                    comboMarca.DataSource = marcas;

                    // Llenar combo servicios
                    query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Servicios () ORDER BY RN ASC";
                    comando.CommandText = query;
                    DataTable servicios = new DataTable();
                    adapter.Fill(servicios);
                    comboServicio.DisplayMember = "NombreServicio";
                    comboServicio.DataSource = servicios;

                    
                }
            }
        }

        private void buttonMasF_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListadoMicro2 otrosFiltros = new ListadoMicro2();
            otrosFiltros.ShowDialog();

            this.fechaDesde = otrosFiltros.fechaDesde;
            this.fechaHasta = otrosFiltros.fechaHasta;
            this.bodegaKGDesde = otrosFiltros.KGDesde;
            this.bodegaKGHasta = otrosFiltros.KGHasta;
            
            this.Show();
        }

    }
}
