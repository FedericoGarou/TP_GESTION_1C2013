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
        private int NroVoucher;

        public EspecificarCompra(int codigoViaje)
        {
            InitializeComponent();
            this.codigoViaje = codigoViaje;
            this.NroVoucher = this.GenerarCompra();
        }

        private int GenerarCompra()
        {
            int NumeroVoucher = -1;
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.GenerarCompra",conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    Console.Out.WriteLine("EL VIAJE ES : " + this.codigoViaje);
                    comando.Parameters.Add("@codigoViaje",SqlDbType.Int).Value = this.codigoViaje;
                    comando.Parameters.Add("@nroVoucher", SqlDbType.Int).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    NumeroVoucher = Convert.ToInt32(comando.Parameters["@nroVoucher"].Value);

                    return NumeroVoucher;
                }
            }

        }

        // PASAJE
        // Calcular el monto del pasaje agregado
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

        private void buttonAddPasaje_Click(object sender, EventArgs e)
        {
            this.Hide();

            AgregarPasaje addPasaje = new AgregarPasaje(this.codigoViaje,this.NroVoucher,"Todos");

            addPasaje.ShowDialog();

            this.ActualizarGridPasajes();
            
            this.Show();
        }

        private void ActualizarGridPasajes()
        {
            String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerPasajesDeUnaCompra( " + this.NroVoucher.ToString() + " , 'TODO' , 0 )";

            using (SqlConnection conexion = this.obtenerConexion())
            {

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable tablaPasajes = new DataTable();
                    adapter.Fill(tablaPasajes);
                    dataGVPasajes.DataSource = tablaPasajes;
                }
            }
        }

        // Eliminar pasaje.
        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasEliminadas = dataGVPasajes.SelectedRows;
            foreach (DataGridViewRow filaEliminada in filasEliminadas)
            {
                //Estoy eliminando un tutor o un atutorado
                if (Convert.ToInt32(filaEliminada.Cells[6].Value) == 0)
                    this.EliminarAtutorado_Tutor(filaEliminada);
                this.eliminarPasaje(filaEliminada);
            }
            
            this.ActualizarGridPasajes();
        }

        private void EliminarAtutorado_Tutor(DataGridViewRow filaEliminada)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarTutorOAtutorado", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.NroVoucher;
                    comando.Parameters.Add("@DNI_Atutorado_Tutor", SqlDbType.Int).Value = filaEliminada.Cells[0].Value;
                    
                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        this.Hide();
                        (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                        this.Show();
                    }

                }
            }
        }

        private void eliminarPasaje(DataGridViewRow filaEliminada)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using(SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarPasajeSinCancelar",conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@codigoViaje",SqlDbType.Int).Value = this.codigoViaje;
		            comando.Parameters.Add("@numeroVoucher",SqlDbType.Int).Value = this.NroVoucher;
                    comando.Parameters.Add("@DNI_pasajero", SqlDbType.Int).Value = filaEliminada.Cells[0].Value;
		            comando.Parameters.Add("@numeroButaca",SqlDbType.Int).Value = filaEliminada.Cells[3].Value;
		            comando.Parameters.Add("@piso",SqlDbType.Int).Value = filaEliminada.Cells[4].Value;
		            comando.Parameters.Add("@ubicacion",SqlDbType.NVarChar).Value = filaEliminada.Cells[5].Value;

                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        this.Hide();
                        (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                        this.Show();
                    }

                }
            }
        }

        
        // ENCOMIENDA
        // Calcular monto del paquete agregado
        private decimal obtenerMontoEncomienda(decimal kilos)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerMontoEncomienda", conexion))
                {
                    conexion.Open();

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@codigoViaje", SqlDbType.Int).Value = this.codigoViaje;
                    comando.Parameters.Add("@monto", SqlDbType.Float).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    return Convert.ToDecimal(comando.Parameters["@monto"].Value) * kilos;

                }
            }
        }
        
        private void buttonAddEncomienda_Click(object sender, EventArgs e)
        {
            this.Hide();

            AgregarEncomienda addEncomienda = new AgregarEncomienda(this.codigoViaje, this.NroVoucher);
            
            addEncomienda.ShowDialog();

            this.actualizarGridEncomiendas();
            
            this.Show();
        }

        private void actualizarGridEncomiendas()
        {
            String query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerEncomiendasDeUnaCompra( " + this.NroVoucher.ToString() + " )";

            using (SqlConnection conexion = this.obtenerConexion())
            {

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable tablaEncomiendas = new DataTable();
                    adapter.Fill(tablaEncomiendas);
                    dataGVEncomienda.DataSource = tablaEncomiendas;
                }
            }
            dataGVEncomienda.Columns[5].Visible = false;
        }

        private void buttonRemEncomienda_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasEliminadas = dataGVEncomienda.SelectedRows;
            foreach (DataGridViewRow filaEliminada in filasEliminadas)
                this.eliminarEncomienda(filaEliminada);
            this.actualizarGridEncomiendas();
            
        }

        private void eliminarEncomienda(DataGridViewRow filaEliminada)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using(SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarEncomiendaSinCancelar",conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@numeroVoucher",SqlDbType.Int).Value = this.NroVoucher;
		            comando.Parameters.Add("@kilosPaquete", SqlDbType.Decimal).Value = filaEliminada.Cells[3].Value;
                    comando.Parameters.Add("@codigoEncomienda", SqlDbType.Int).Value = filaEliminada.Cells[5].Value;

                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        this.Hide();
                        (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                        this.Show();
                    }

                }
            }
        }

        // DEL FORMULARIO
        //Cancelar
        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.cancelarCompra();
            this.Close();
        }

        private void EspecificarCompra_FormClosing(Object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.cancelarCompra();
            this.Close();
        }

        private void cancelarCompra()
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_CancelarCompraSinDevolver", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.NroVoucher;

                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        this.Hide();
                        (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                        this.Show();
                    }

                }
            }
        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Especificar quien paga.
            EspecificarPago addPago = new EspecificarPago();
            DialogResult dr = addPago.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.EstablecerPago(addPago);
                (new Comprobante(this.NroVoucher)).ShowDialog();
                this.Close();
            }
            else
                this.Show();

        }

        private void EstablecerPago(EspecificarPago pago)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.SP_EstablecerPago", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@numeroVoucher", SqlDbType.Int).Value = this.NroVoucher;
                    comando.Parameters.Add("@DNI_Pago",SqlDbType.Int).Value = pago.DNI_Abonante;
	                comando.Parameters.Add("@TipoPago",SqlDbType.NVarChar).Value = pago.tipoPago;
	                comando.Parameters.Add("@NumeroTarjetaPago",SqlDbType.Int).Value = pago.numeroTarjeta;
	                comando.Parameters.Add("@ClaveTarjetaPago",SqlDbType.NVarChar).Value = pago.claveTarjeta;
	                comando.Parameters.Add("@CompaniaTarjetaPago",SqlDbType.NVarChar).Value = pago.companiaTarjeta;

                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        this.Hide();
                        (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                        this.Show();
                    }

                }
            }
        }

        private void EspecificarCompra_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        
        }

    }
}
