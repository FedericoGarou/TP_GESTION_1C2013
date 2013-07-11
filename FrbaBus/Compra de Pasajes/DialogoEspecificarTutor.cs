using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class DialogoEspecificarTutor : Form1
    {
        private int numeroVoucher;
        private int codigoViaje;
        private int DNI_Tutoreado;
        
        public DialogoEspecificarTutor(int numeroVoucher,int codigoViaje,int DNI_Atutorado)
        {
            InitializeComponent();
            this.numeroVoucher = numeroVoucher;
            this.codigoViaje = codigoViaje;
            this.DNI_Tutoreado = DNI_Atutorado;
            DataGridViewButtonColumn botonesTutor = this.crearBotones("Elegir tutor", "Elegir");
            dataGVExistentes.Columns.Add(botonesTutor);
            dataGVExistentes.Columns[0].Visible = false;
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            this.Hide();
            dataGVExistentes.DataSource = null;
            dataGVExistentes.Columns[0].Visible = false;
            AgregarPasaje addPasaje = new AgregarPasaje(this.codigoViaje, this.numeroVoucher, "Por tutor");
            addPasaje.ShowDialog();
            this.Close();
        }

        private void buttonExistente_Click(object sender, EventArgs e)
        {
            String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerPasajesDeUnaCompra( " + this.numeroVoucher.ToString() + " , 'SINTUTOR' , " + this.DNI_Tutoreado.ToString() + ")";

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable tablaPasajes = new DataTable();
                    adapter.Fill(tablaPasajes);
                    dataGVExistentes.DataSource = tablaPasajes;
                    dataGVExistentes.Columns[0].Visible = true;
                }
            }
        }

        private void dataGVExistentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGVExistentes.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.EstablecerTutor", conexion))
                        {
                            conexion.Open();
                            comand.CommandType = CommandType.StoredProcedure;

                            comand.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.numeroVoucher;
                            comand.Parameters.Add("@DNI_Tutor", SqlDbType.Int).Value = Convert.ToInt32(dataGVExistentes.Rows[e.RowIndex].Cells["DNI"].Value);
                            
                            comand.ExecuteNonQuery();
                        }
                    }

                    this.Close();
                }

            }
            catch (SqlException ex)
            {
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
            }
            
        }

        private int obtenerCodigoButaca(int numero, int codViaje)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerCodigoButaca", conexion))
                {
                    conexion.Open();

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
                    comando.Parameters.Add("@numeroButaca", SqlDbType.Int).Value = numero;
                    comando.Parameters.Add("@codigoButaca", SqlDbType.Int).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    return Convert.ToInt32(comando.Parameters["@codigoButaca"].Value);

                }
            }
        }

        
    }
}
