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
    public partial class DialogoMicroFSTieneViajes : Form1
    {
        private String patenteMicro;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public DialogoMicroFSTieneViajes(String patente, DateTime fechaIni,DateTime fechaFin)
        {
            InitializeComponent();
            this.patenteMicro = patente;
            this.fechaInicio = fechaIni;
            this.fechaFin = fechaFin;
        }

        public void buttonCancelarTodo_Click(object sender, EventArgs e)
        {
            buttonCancelarTodo.Enabled = false;
            buttonReemplazar.Enabled = false;
            
            using (SqlConnection conexion = this.obtenerConexion())
            {
                String query = "SELECT * "+
                               "FROM LOS_VIAJEROS_DEL_ANONIMATO.ComprasDelMicroEnPeriodo("+
                               "'"+this.patenteMicro+"',"+
                               "'"+this.fechaInicio.ToString("yyyyMMdd")+"',"+
                               "'" + this.fechaFin.ToString("yyyyMMdd") + "')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    
                    SqlDataReader reader = comando.ExecuteReader();

                    while(reader.Read())
                        this.cancelarCompra(reader[0]);

                }
            }
            
            buttonCancelarTodo.Enabled = true;
            buttonReemplazar.Enabled = true;
            this.Close();

        }

        private void cancelarCompra(object Voucher)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.CancelarCompra", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@numVoucher", SqlDbType.Int).Value = Convert.ToInt32(Voucher);
                    comando.ExecuteNonQuery();

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void DialogoMicroFSTieneViajes_Load(object sender, EventArgs e)
        {
           
        }

        private void pBar_Click(object sender, EventArgs e)
        {

        }

        private void buttonReemplazar_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new ReemplazarMicro(this.patenteMicro, this.fechaInicio, this.fechaFin)).ShowDialog();
            this.Close();
        }

        
    }
}
