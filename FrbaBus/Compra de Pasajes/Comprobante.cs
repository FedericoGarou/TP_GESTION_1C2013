using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class Comprobante : Form
    {
        private int numeroVoucher;

        public Comprobante(int numVou)
        {
            InitializeComponent();
            this.numeroVoucher = numVou;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Comprobante_Load(object sender, EventArgs e)
        {
            using (SqlConnection conexion = this.generarConexion())
            {
                using (SqlCommand comando = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.ObtenerDatosDeCompra", conexion))
                {
                    conexion.Open();
                    comando.CommandType = CommandType.StoredProcedure;  

                    comando.Parameters.Add("@numeroVoucher",SqlDbType.Int).Value = this.numeroVoucher;
	                comando.Parameters.Add("@origen",SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@destino",SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@servicio",SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@fechaSalida",SqlDbType.DateTime).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@fechaLlegada",SqlDbType.DateTime).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@patente",SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@cantidadPasajes",SqlDbType.Int,18).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@kilosEncomiendas",SqlDbType.Decimal,20).Direction = ParameterDirection.Output;
	                comando.Parameters.Add("@total",SqlDbType.Decimal).Direction = ParameterDirection.Output;

                    comando.ExecuteNonQuery();

                    textOrigen.Text = comando.Parameters["@origen"].Value.ToString();
                    textDestino.Text = comando.Parameters["@destino"].Value.ToString();
                    textServicio.Text = comando.Parameters["@servicio"].Value.ToString();
                    textFechaSal.Text = Convert.ToDateTime(comando.Parameters["@fechaSalida"].Value).ToString();
                    textFechaLlegada.Text = Convert.ToDateTime(comando.Parameters["@fechaLlegada"].Value).ToString();
                    textPatente.Text = comando.Parameters["@patente"].Value.ToString();
                    textCantidadPasj.Text = comando.Parameters["@cantidadPasajes"].Value.ToString();
                    textKilos.Text = Convert.ToDecimal(comando.Parameters["@kilosEncomiendas"].Value).ToString();
                    textTotal.Text = Convert.ToDecimal(comando.Parameters["@total"].Value).ToString();
                    textNumVoucher.Text = this.numeroVoucher.ToString();

                }
            }
        }

        private SqlConnection generarConexion()
        {
            String stringDeConexion = "";
            String linea;

            String ruta = Application.StartupPath+@"\FRBABUS.txt";
            
            StreamReader archivo = new StreamReader(ruta,Encoding.ASCII);

            while ((linea = archivo.ReadLine()) != null)
            {
                String[] renglon = linea.Split(':');
                String primerPalabra = renglon[0];

                if (primerPalabra.Equals("conexion"))
                    stringDeConexion = renglon[1];
            }
            
            archivo.Close();

            return new SqlConnection(stringDeConexion);
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
