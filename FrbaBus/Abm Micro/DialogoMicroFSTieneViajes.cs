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
        private String tipoDeBaja;
        private String patenteMicro;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public DialogoMicroFSTieneViajes(String tipoBaja,String patente, DateTime fechaIni,DateTime fechaFin)
        {
            InitializeComponent();
            this.tipoDeBaja = tipoBaja;
            this.patenteMicro = patente;
            this.fechaInicio = fechaIni;
            this.fechaFin = fechaFin;
        }

        public DialogoMicroFSTieneViajes(String tipoBaja, String patente, DateTime fechaIni)
        {
            InitializeComponent();
            this.tipoDeBaja = tipoBaja;
            this.patenteMicro = patente;
            this.fechaInicio = fechaIni;
        }

        public void buttonCancelarTodo_Click(object sender, EventArgs e)
        {
            buttonCancelarTodo.Enabled = false;
            buttonReemplazar.Enabled = false;
            String textoAnterior = label1.Text;
            label1.Text = "Espere por favor. Estamos ejecutando la acción que eligió. Esta operación puede tardar mucho tiempo. No cierre el programa";
            this.Enabled = false;
            
            if (this.tipoDeBaja.Equals("PorModificacion"))
                this.cancelarTodoPorModificacion();
            else
                this.cancelarTodoDefinitivamente();

            label1.Text = textoAnterior;
            this.Enabled = true;

            buttonCancelarTodo.Enabled = true;
            buttonReemplazar.Enabled = true;
            this.Close();

        }

        private void cancelarTodoDefinitivamente()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                String query = "SELECT * " +
                               "FROM LOS_VIAJEROS_DEL_ANONIMATO.ComprasDelMicroEnPeriodo_Def(" +
                               "'" + this.patenteMicro + "'," +
                               "'" + this.getFechaActual().ToString("yyyyMMdd") + "')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                        this.cancelarCompra(reader[0]);

                }
            }
        }

        private void cancelarTodoPorModificacion()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                String query = "SELECT * " +
                               "FROM LOS_VIAJEROS_DEL_ANONIMATO.ComprasDelMicroEnPeriodo(" +
                               "'" + this.patenteMicro + "'," +
                               "'" + this.fechaInicio.ToString("yyyyMMdd") + "'," +
                               "'" + this.fechaFin.ToString("yyyyMMdd") + "')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                        this.cancelarCompra(reader[0]);

                }
            }
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

        private void buttonReemplazar_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (this.tipoDeBaja.Equals("PorModificacion"))
                (new ReemplazarMicro(this.tipoDeBaja,this.patenteMicro, this.fechaInicio, this.fechaFin)).ShowDialog();
            else
                (new ReemplazarMicro(this.tipoDeBaja,this.patenteMicro, this.fechaInicio)).ShowDialog();
            this.Close();
        }

        
    }
}
