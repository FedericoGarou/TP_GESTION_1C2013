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
    public partial class ModifMicro : Form1
    {
        public ModifMicro(string unaPatente)
        {
            InitializeComponent();
            label2.Text = unaPatente;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                
                conexion.Open();
                DataTable tabla = new DataTable();

                cargarATablaParaDataGripView("USE GD1C2013 select Patente, ma.Marca, KG_Disponibles, Cantidad_Butacas from los_viajeros_del_anonimato.MICRO mi join LOS_VIAJEROS_DEL_ANONIMATO.MARCA ma on (mi.Marca = ma.Id_Marca) WHERE Patente = '"+unaPatente+"'", ref tabla, conexion);

                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[0].ReadOnly = true;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new ModifMarca(label2.Text)).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_TieneViajesProgramados_Def", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@patente", SqlDbType.NVarChar).Value = label2.Text;
                    comando.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = getFechaActual();                    
                    comando.Parameters.Add("@tieneViajes", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    Boolean tieneViajes = Convert.ToBoolean(comando.Parameters["@tieneViajes"].Value);

                    if (tieneViajes)
                    {
                        this.Hide();
                        (new Dialogo("El micro tiene viajes programados;No se puede modificar butacas/kgs", "Aceptar")).ShowDialog();
                    }
                    else
                    {
                        (new ModifButacas(label2.Text)).Show();
                    }                   

                }
            }
        }
    }
}
