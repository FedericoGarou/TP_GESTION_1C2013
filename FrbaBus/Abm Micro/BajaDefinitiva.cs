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
    public partial class BajaDefinitiva : Form1
    {
        public BajaDefinitiva()
        {
            InitializeComponent();
        }

        private void BajaDefinitiva_Load(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Patentes() ORDER BY RN ASC", conexion))
                {
                    conexion.Open();
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable patentes = new DataTable();
                    adapter.Fill(patentes);
                    comboPatentes.DisplayMember = "Patente";
                    comboPatentes.DataSource = patentes;

                }
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (comboPatentes.SelectedIndex == 0)
            {
                this.Hide();
                (new Dialogo("No se seleccionó ningún micro", "Aceptar")).ShowDialog();
                this.Show();
            }
            else
            {
                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_DarDeBajaDefinitivamente", conexion))
                    {
                        conexion.Open();
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = comboPatentes.Text;
                        comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = this.getFechaActual();

                        comando.ExecuteNonQuery();

                        this.comprobarViajes();

                        this.Close();

                    }
                }
            }
        }

        private void comprobarViajes()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_TieneViajesProgramados_Def", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = comboPatentes.Text;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = this.getFechaActual();
                    comando.Parameters.Add("@tieneViajes", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    Boolean tieneViajes = Convert.ToBoolean(comando.Parameters["@tieneViajes"].Value);

                    if (tieneViajes)
                    {
                        this.Hide();
                        (new DialogoMicroFSTieneViajes("Definitivamente", comboPatentes.Text,this.getFechaActual())).ShowDialog();
                    }

                    this.Close();

                }
            }
        }
    }
}
