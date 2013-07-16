using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Cancelar_Viaje
{
    public partial class EspecificarCancelacion : Form1
    {
        public int nroVoucher;
        public int codigoDevolucion;

        public EspecificarCancelacion(int voucher)
        {
            InitializeComponent();
            this.nroVoucher = voucher;
        }

        private void EspecificarCancelacion_Load(object sender, EventArgs e)
        {
            this.codigoDevolucion = this.generarDevolucion();

            textMotivo.MaxLength = 255;

            listListadoCodigos.DisplayMember = "CodigoCompra";

            String QueryPasajes = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_PasajesDeVoucher(" + this.nroVoucher.ToString() + ")";
            String QueryEncomiendas = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_EncomiendasDeVoucher(" + this.nroVoucher.ToString() + ")";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.Connection = conexion;
                    
                    cmd.CommandText = QueryPasajes;
                    DataTable pasajesDeVoucher = new DataTable();
                    adapter.Fill(pasajesDeVoucher);
                    comboCodPasaje.DisplayMember = "CodigoCompra";
                    comboCodPasaje.DataSource = pasajesDeVoucher;

                    cmd.CommandText = QueryEncomiendas;
                    DataTable encomiendasDeVoucher = new DataTable();
                    adapter.Fill(encomiendasDeVoucher);
                    comboCodEncomienda.DisplayMember = "CodigoCompra";
                    comboCodEncomienda.DataSource = encomiendasDeVoucher;
                }
            }
        }

        private int generarDevolucion()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.GenerarDevolucion",conexion))
                {
                    conexion.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@codigoDev", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    return Convert.ToInt32(cmd.Parameters["@codigoDev"].Value);
                }
            }
        }

        private void comboCodEncomienda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCodEncomienda.Text.Equals("-1"))
            {
                comboCodEncomienda.Text = "No seleccionado";
            }
        }

        private void comboCodPasaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCodPasaje.Text.Equals("-1"))
            {
                comboCodPasaje.Text = "No seleccionado";
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAddEncomienda_Click(object sender, EventArgs e)
        {
            if (comboCodEncomienda.SelectedIndex != 0)
            {
                if (!listListadoCodigos.Items.Contains(comboCodEncomienda.SelectedItem))
                    listListadoCodigos.Items.Add(comboCodEncomienda.SelectedItem);
            }
        }

        private void buttonAddPasaje_Click(object sender, EventArgs e)
        {
            if (comboCodPasaje.SelectedIndex != 0)
            {
                if (!listListadoCodigos.Items.Contains(comboCodPasaje.SelectedItem))
                    listListadoCodigos.Items.Add(comboCodPasaje.SelectedItem);
            }
        }

        private void buttonDelEncomienda_Click(object sender, EventArgs e)
        {
            listListadoCodigos.Items.RemoveAt(listListadoCodigos.SelectedIndex);
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            listListadoCodigos.Items.Clear();
            textMotivo.Text = "";
            comboCodEncomienda.SelectedIndex = 0;
            comboCodPasaje.SelectedIndex = 0;
        }

        public void validarParametros()
        {
            Boolean hayError = false;
            String errorMessage = "";

            if (textMotivo.Text == "")
            {
                hayError = true;
                errorMessage += "Debe especificar un motivo;";
            }

            if (listListadoCodigos.Items.Count == 0)
            {
                hayError = true;
                errorMessage += "No se especificó ningun pasaje/encomienda;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(errorMessage);


        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarParametros();

                foreach (System.Data.DataRowView valor in listListadoCodigos.Items)
                {
                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        using (SqlCommand cmd = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.Insertar_Devolucion",conexion))
                        {
                            conexion.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@motivo", SqlDbType.NVarChar).Value = textMotivo.Text;
                            cmd.Parameters.Add("@codigoCompra", SqlDbType.Int).Value = valor["CodigoCompra"];
                            cmd.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.nroVoucher;
                            cmd.Parameters.Add("@codigoDev", SqlDbType.Int).Value = this.codigoDevolucion;

                            cmd.ExecuteNonQuery();
                            
                        }
                    }
                }

                this.Close();

            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
                this.Show();
            }
        }
    }
}
