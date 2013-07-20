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
    public partial class ListadoMicro2 : Form1
    {
        public DateTime fechaDesde;
        public DateTime fechaHasta;
        public int KGDesde = 0;
        public int KGHasta = 0;

        public ListadoMicro2()
        {
            InitializeComponent();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            numericKGDesde.Value = 0;
            numericKGHasta.Value = 0;
            textFecDesde.Text = "";
            textFecHasta.Text = "";
        }

        private void validarParametros()
        {
            Boolean hayError = false;
            String mensajeError = "";

            if (numericKGDesde.Value > numericKGHasta.Value)
            {
                hayError = true;
                mensajeError += "El campo kilos desde debe ser mayor o igual a kilos desde;";
            }

            if (fechaDesde != DateTime.MinValue)
            {
                if (fechaHasta == DateTime.MinValue )
                {
                    hayError = true;
                    mensajeError += "Debe elegirse un periodo acotado de tiempo;";
                }

            }

            if (fechaDesde > fechaHasta)
            {
                hayError = true;
                mensajeError += "La fecha de inicio debe ser mayor a la fecha de fin del periodo libre;";
            }

            if (fechaHasta != DateTime.MinValue)
            {
                if (fechaDesde == DateTime.MinValue)
                {
                    hayError = true;
                    mensajeError += "Debe elegirse un periodo acotado de tiempo;";
                }

            }

            if (hayError)
                throw new ParametrosIncorrectosException(mensajeError);
        }
        
        private void buttonFecDesde_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textFecDesde.Text = ex.Message;
                fechaDesde = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

        private void buttonFecHasta_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textFecHasta.Text = ex.Message;
                fechaHasta = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarParametros();
                this.Close();
            }
            catch(ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void numericKGDesde_ValueChanged(object sender, EventArgs e)
        {
            this.KGDesde = Convert.ToInt32(numericKGDesde.Value);
        }

        private void numericKGHasta_ValueChanged(object sender, EventArgs e)
        {
            this.KGHasta = Convert.ToInt32(numericKGHasta.Value);
        }

        private void buttonFinal_Click(object sender, EventArgs e)
        {
            try 
            {
                this.validarParametros();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand())
                    {

                        String fechaStDesde = this.fechaDesde.ToString("yyyyMMdd");
                        String fechaStHasta = this.fechaHasta.ToString("yyyyMMdd");
    
                        if(this.fechaDesde == DateTime.MinValue)
                            fechaStDesde = "20120101";
                        if(this.fechaHasta == DateTime.MinValue)
                            fechaStHasta = "20120101";
    
                        String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.AplicarFiltros2("+
                                    "'"+fechaStDesde+"',"+
                                    "'"+fechaStHasta+"',"+
                                    KGDesde.ToString()+","+
                                    KGHasta.ToString()+")";

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

        private void ListadoMicro2_Load(object sender, EventArgs e)
        {
            this.fechaDesde = DateTime.MinValue;
            this.fechaHasta = DateTime.MinValue;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }
    }
}
