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
    public partial class BajaFueraDeServicio : Form1
    {
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public BajaFueraDeServicio()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BajaFueraDeServicio_Load(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_PatentesTodas() ORDER BY RN ASC", conexion))
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

        private void buttonFecIni_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textFecInicio.Text = ex.Message;
                fechaInicio = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

        private void buttonFecFin_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                (new CalendarioCompra()).ShowDialog();
            }
            catch (FechaElegidaExeption ex)
            {
                textFecFin.Text = ex.Message;
                fechaFin = Convert.ToDateTime(ex.Message);
            }
            finally
            {
                this.Show();
                this.Focus();
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            comboPatentes.SelectedIndex = 0;
            textFecFin.Text = "";
            textFecInicio.Text = "";
        }

        private void validarDatos()
        {
            Boolean hayError = false;
            String mensajeError = "";

            if (comboPatentes.SelectedIndex == 0)
            {
                hayError = true;
                mensajeError += "No se seleccionó ninguna patente;";
            }

            if (textFecInicio.Text.Equals(""))
            {
                hayError = true;
                mensajeError += "No se seleccionó fecha de inicio;";
            }

            if (textFecFin.Text.Equals(""))
            {
                hayError = true;
                mensajeError += "No se seleccionó fecha de fin;";
            }

            if (fechaInicio < this.getFechaActual())
            {
                hayError = true;
                mensajeError += "La fecha de inicio debe ser igual o posterior a hoy;";
            }

            if (fechaInicio > fechaFin)
            {
                hayError = true;
                mensajeError += "La fecha de inicio debe ser anterior a la fecha de fin;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(mensajeError);

        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarDatos();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_DarDeBajaPorMantenimiento", conexion))
                    {
                        conexion.Open();
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = comboPatentes.Text;
                        comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                        comando.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;

                        comando.ExecuteNonQuery();

                        this.comprobarViajes();

                        this.Close();

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

        private void comprobarViajes()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_TieneViajesProgramados", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = comboPatentes.Text;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                    comando.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;
                    comando.Parameters.Add("@tieneViajes", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    Boolean tieneViajes = Convert.ToBoolean(comando.Parameters["@tieneViajes"].Value);

                    if (tieneViajes)
                    {
                        this.Hide();
                        (new DialogoMicroFSTieneViajes("PorModificacion", comboPatentes.Text, fechaInicio, fechaFin)).ShowDialog();
                    }

                    this.Close();

                }
            }
            
        }
        
    }
}
