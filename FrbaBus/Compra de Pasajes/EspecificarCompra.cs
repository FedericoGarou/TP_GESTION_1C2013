using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class EspecificarCompra : Form1
    {
        private int codigoViaje;
        private ArrayList PASAJEROS_ARRAY = new ArrayList();
        private ArrayList PASAJES_ARRAY = new ArrayList();

        public EspecificarCompra(int codigoViaje)
        {
            InitializeComponent();
            this.codigoViaje = codigoViaje;
        }

        private void buttonAddPasaje_Click(object sender, EventArgs e)
        {
            decimal monto = 0;
            this.Hide();
            AgregarPasaje addPasaje = new AgregarPasaje(this.codigoViaje);
            DialogResult dr = addPasaje.ShowDialog();
            int codigoButacaSeleccionada = this.obtenerCodigoButaca(addPasaje.numeroButaca,this.codigoViaje);
            if (dr == DialogResult.OK)
            {
                // Controlar que el usuario no compre más de un pasaje y que un asiento no sea comprado más de dos veces
                if ((!this.PASAJEROS_ARRAY.Contains(addPasaje.DNI_Pasajero)) && (!this.PASAJES_ARRAY.Contains(codigoButacaSeleccionada)))
                {
                    this.PASAJEROS_ARRAY.Add(addPasaje.DNI_Pasajero);
                    this.PASAJES_ARRAY.Add(codigoButacaSeleccionada);

                    monto = this.obtenerMonto();

                    dataGVPasajes.Rows.Add(
                        addPasaje.DNI_Pasajero,
                        addPasaje.ApellidoPasajero,
                        addPasaje.NombrePasajero,
                        addPasaje.numeroButaca,
                        addPasaje.pisoButaca,
                        addPasaje.ubicacionButaca,
                        monto);

                    textBoxTotal.Text = (Convert.ToDecimal(textBoxTotal.Text) + monto).ToString();
                }
                else
                {
                    this.Hide();
                    String errorMessage = "";

                    if (this.PASAJEROS_ARRAY.Contains(addPasaje.DNI_Pasajero))
                        errorMessage += "Este cliente ya ha comprado un pasaje en esta sesión;";
                    if (this.PASAJES_ARRAY.Contains(codigoButacaSeleccionada))
                        errorMessage += "Este pasaje ya ha sido seleccionado en esta sesión;";

                    (new Dialogo(errorMessage, "Aceptar")).ShowDialog();
                }
            }
            
            this.Show();
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

        private decimal obtenerMonto()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerMontoPasaje", conexion))
                {
                    conexion.Open();

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
                    comando.Parameters.Add("@monto", SqlDbType.Float).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    return Convert.ToDecimal(comando.Parameters["@monto"].Value);

                }
            }
        }

        // Eliminar pasaje.
        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasEliminadas = dataGVPasajes.SelectedRows;
            foreach (DataGridViewRow filaEliminada in filasEliminadas)
            {
                int butacaEliminada = this.obtenerCodigoButaca( Convert.ToInt32(filaEliminada.Cells[3].Value) , this.codigoViaje);
                
                ArrayList auxiliarPasajes = new ArrayList();
                foreach (int codigo in PASAJES_ARRAY)
                {
                    if (codigo != butacaEliminada)
                        auxiliarPasajes.Add(codigo);
                }
                PASAJES_ARRAY = auxiliarPasajes;

                ArrayList auxiliarPasajeros = new ArrayList();
                foreach (int DNI in PASAJEROS_ARRAY)
                {
                    if (DNI != Convert.ToInt32(filaEliminada.Cells[0].Value))
                        auxiliarPasajeros.Add(DNI);
                }
                PASAJEROS_ARRAY = auxiliarPasajeros;

                textBoxTotal.Text = (Convert.ToDecimal(textBoxTotal.Text) - Convert.ToDecimal(filaEliminada.Cells["MontoPasaje"].Value)).ToString();

                dataGVPasajes.Rows.RemoveAt(filaEliminada.Index);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }




    }
}
