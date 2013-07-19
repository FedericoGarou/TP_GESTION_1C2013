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
    public partial class ReemplazarMicro : Form1
    {
        private String tipoDeBaja;
        private String patenteMicro;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public ReemplazarMicro(String tipoDeBaja,String patente,DateTime fechaInicio,DateTime fechaFin)
        {
            InitializeComponent();
            this.tipoDeBaja = tipoDeBaja;
            this.patenteMicro = patente;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
        }

        public ReemplazarMicro(String tipoDeBaja, String patente, DateTime fechaInicio)
        {
            InitializeComponent();
            this.tipoDeBaja = tipoDeBaja;
            this.patenteMicro = patente;
            this.fechaInicio = fechaInicio;
        }

        private void ReemplazarMicro_Load(object sender, EventArgs e)
        {

            if (this.NoExisteMicroCoincidente())
            {
                this.Hide();
                (new NoExisteMicro()).ShowDialog();
                this.Close();
            }
            else
            {
                DataGridViewButtonColumn botonesReemplazar = this.crearBotones("Reemplazar", "Reemplazar");
                dataGridView1.Columns.Add(botonesReemplazar);

                if (this.tipoDeBaja.Equals("PorModificacion"))
                    this.MostrarMicrosParaMantenimiento();
                else
                    this.MostrarMicrosDefinitiva();

                
                
            }
        }

        private void MostrarMicrosDefinitiva()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_MicrosDeReemplazo_Def(" +
                               "'" + this.patenteMicro.ToString() + "'," +
                               "'" + this.getFechaActual().ToString("yyyyMMdd") + "')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable microsReemplazo = new DataTable();
                    adapter.Fill(microsReemplazo);
                    dataGridView1.DataSource = microsReemplazo;

                }
            }
        }

        private void MostrarMicrosParaMantenimiento()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_MicrosDeReemplazo(" +
                               "'" + this.patenteMicro.ToString() + "'," +
                               "'" + this.fechaInicio.ToString("yyyyMMdd") + "'," +
                               "'" + this.fechaFin.ToString("yyyyMMdd") + "')";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable microsReemplazo = new DataTable();
                    adapter.Fill(microsReemplazo);
                    dataGridView1.DataSource = microsReemplazo;

                }
            }
        }

        private bool NoExisteMicroCoincidente()
        {
            if (this.tipoDeBaja.Equals("PorModificacion"))
                return this.NoExisteMicroParaModificacion();
            else
                return this.noexistemicroparadefinitiva();

        }

        private bool noexistemicroparadefinitiva()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.HayMicroParaReemplazo_Def", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = this.patenteMicro;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = this.getFechaActual();
                    comando.Parameters.Add("@hayMicros", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    comando.ExecuteNonQuery();

                    return !Convert.ToBoolean(comando.Parameters["@hayMicros"].Value);

                }
            }
        }

        private bool NoExisteMicroParaModificacion()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.HayMicroParaReemplazo", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = this.patenteMicro;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = this.fechaInicio;
                    comando.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = this.fechaFin;
                    comando.Parameters.Add("@hayMicros", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    comando.ExecuteNonQuery();

                    return !Convert.ToBoolean(comando.Parameters["@hayMicros"].Value);

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String patenteReemplazo = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() ;
                if (this.tipoDeBaja.Equals("PorModificacion"))
                    this.ReemplazarPorModificacion(patenteReemplazo);
                else
                    this.ReemplazarDefinitivamente(patenteReemplazo);

                

                this.Close();
            }
        }

        private void ReemplazarDefinitivamente(string patenteReemplazo)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.ReemplazarMicro_Def", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patenteReemplazado", SqlDbType.NVarChar).Value = this.patenteMicro;
                    comando.Parameters.Add("@patenteReemplazo", SqlDbType.NVarChar).Value = patenteReemplazo;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = this.fechaInicio;
                    
                    comando.ExecuteNonQuery();

                }
            }
        }

        private void ReemplazarPorModificacion(string patenteReemplazo)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.ReemplazarMicro", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patenteReemplazado", SqlDbType.NVarChar).Value = this.patenteMicro;
                    comando.Parameters.Add("@patenteReemplazo", SqlDbType.NVarChar).Value = patenteReemplazo;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = this.fechaInicio;
                    comando.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = this.fechaFin;

                    comando.ExecuteNonQuery();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
