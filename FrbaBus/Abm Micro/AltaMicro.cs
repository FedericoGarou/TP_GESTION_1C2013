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
    public partial class AltaMicro : Form1
    {
        public AltaMicro()
        {
            InitializeComponent();
        }

        private void AltaMicro_Load(object sender, EventArgs e)
        {
            String query = "";

            DataGridViewButtonColumn botonesEliminarButVen = this.crearBotones("Eliminar butaca", "Eliminar");
            dataGVButVent.Columns.Add(botonesEliminarButVen);
            DataGridViewButtonColumn botonesEliminarButPis = this.crearBotones("Eliminar butaca", "Eliminar");
            dataGVButPis.Columns.Add(botonesEliminarButPis);

            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand())
                {
                    conexion.Open();
                    comando.Connection = conexion;
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);

                    // Llenar combo marca
                    query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Marcas() ORDER BY RN ASC";
                    comando.CommandText = query;
                    DataTable marcas = new DataTable();
                    adapter.Fill(marcas);
                    comboMarca.DisplayMember = "Marca";
                    comboMarca.DataSource = marcas;

                    // Llenar combo servicios
                    query = "SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.F_Servicios () ORDER BY RN ASC";
                    comando.CommandText = query;
                    DataTable servicios = new DataTable();
                    adapter.Fill(servicios);
                    comboServicio.DisplayMember = "NombreServicio";
                    comboServicio.DataSource = servicios;

                    // Textos de butacas
                    textButPas.Text = "0";
                    textButVent.Text = "0";

                }
            }
        }

        private void buttonButVent_Click(object sender, EventArgs e)
        {
            this.Hide();
            EspecificarPiso EP = new EspecificarPiso();
            DialogResult dr = EP.ShowDialog();
            if (dr == DialogResult.OK)
            {
                dataGVButVent.Rows.Add(new object[]{EP.piso});
                textButVent.Text = (Convert.ToInt32(textButVent.Text) + 1).ToString();
                this.Show();
            }
        }

        private void buttonButPas_Click(object sender, EventArgs e)
        {
            this.Hide();
            EspecificarPiso EP = new EspecificarPiso();
            DialogResult dr = EP.ShowDialog();
            if (dr == DialogResult.OK)
            {
                dataGVButPis.Rows.Add(new object[] { EP.piso });
                textButPas.Text = (Convert.ToInt32(textButPas.Text) + 1).ToString();
                this.Show();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textPatente.Text = "";
            comboMarca.SelectedIndex = 0;
            textModelo.Text = "";
            comboServicio.SelectedIndex = 0;
            numericKG.Value = 0;
            textButPas.Text = "0";
            textButVent.Text = "0";
            dataGVButPis.Rows.Clear();
            dataGVButVent.Rows.Clear();
        }

        private void dataGVButVent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVButVent.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                dataGVButVent.Rows.RemoveAt(e.RowIndex);
                textButVent.Text = (Convert.ToInt32(textButVent.Text) - 1).ToString();
            }
        }

        private void dataGVButPis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVButPis.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                dataGVButPis.Rows.RemoveAt(e.RowIndex);
                textButPas.Text = (Convert.ToInt32(textButPas.Text) - 1).ToString();
            }
        }

        private void validarParametros()
        {
            Boolean hayError = false;
            String mensajeError = "";

            if (textPatente.Text.Equals(""))
            {
                hayError = true;
                mensajeError += "No se seleccionó la patente del micro;";
            }

            if (comboMarca.SelectedIndex == 0)
            {
                hayError = true;
                mensajeError += "No se seleccionó la marca del micro;";
            }

            if (textModelo.Text.Equals(""))
            {
                hayError = true;
                mensajeError += "No se seleccionó el modelo del micro;";
            }

            if (comboServicio.SelectedIndex == 0)
            {
                hayError = true;
                mensajeError += "No se seleccionó el servicio del micro;";
            }

            if (numericKG.Value == 0)
            {
                hayError = true;
                mensajeError += "No se seleccionó la capacidad del micro;";
            }

            if (textButPas.Text.Equals("0") && textButVent.Text.Equals("0"))
            {
                hayError = true;
                mensajeError += "No se agregó ninguna butaca;";
            }

            if (hayError)
                throw new ParametrosIncorrectosException(mensajeError);
        }

        private void buttonFinal_Click(object sender, EventArgs e)
        {
            try 
            {
                this.validarParametros();

                using (SqlConnection conexion = this.obtenerConexion())
                {
                    using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.DarDeAltaMicro",conexion))
                    {
                        conexion.Open();
                        comand.CommandType = CommandType.StoredProcedure;

                        comand.Parameters.Add("@patente",SqlDbType.NVarChar).Value = textPatente.Text;
	                    comand.Parameters.Add("@marca",SqlDbType.NVarChar).Value = comboMarca.Text;
                	    comand.Parameters.Add("@modelo",SqlDbType.NVarChar).Value = textModelo.Text;
	                    comand.Parameters.Add("@fechaAlta",SqlDbType.DateTime).Value = this.getFechaActual();
	                    comand.Parameters.Add("@servicio",SqlDbType.NVarChar).Value = comboServicio.Text;
	                    comand.Parameters.Add("@KG_bodega",SqlDbType.Decimal).Value = numericKG.Value;
                        comand.Parameters.Add("@CantidadButacas", SqlDbType.Int).Value = (Convert.ToInt32(textButPas.Text)) + (Convert.ToInt32(textButVent.Text));
                        
                        comand.ExecuteNonQuery();

                        this.insertarButacas();

                        buttonLimpiar.PerformClick();

                    }
                }
            }
            catch (ParametrosIncorrectosException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message,"Aceptar")).ShowDialog();
                this.Show();
            }
            catch(SqlException ex)
            {
                this.Hide();
                (new Dialogo(ex.Message, "Aceptar")).ShowDialog();
                this.Show();
            }
        }

        private void insertarButacas()
        {
            int numero = 1;

            DataGridViewRowCollection butacasPasillo = dataGVButPis.Rows;
            foreach (DataGridViewRow pasillo in butacasPasillo)
            {
                this.persistirButacas("Pasillo", ref numero, Convert.ToInt32(pasillo.Cells[0].Value));
                numero++;
            }
            
            DataGridViewRowCollection butacasVentanilla = dataGVButVent.Rows;
            foreach (DataGridViewRow ventanilla in butacasVentanilla)
            {
                this.persistirButacas("Ventanilla", ref numero, Convert.ToInt32(ventanilla.Cells[0].Value));
                numero++;
            }

        }

        private void persistirButacas(string ubicacion,ref int numeroB,int pisoB)
        {
            using (SqlConnection conexion = this.obtenerConexion())
            {
                using (SqlCommand comand = new SqlCommand("LOS_VIAJEROS_DEL_ANONIMATO.DarDeAltaButaca", conexion))
                {
                    conexion.Open();
                    comand.CommandType = CommandType.StoredProcedure;

                    comand.Parameters.Add("@patente",SqlDbType.NVarChar).Value = textPatente.Text;
                	comand.Parameters.Add("@numero", SqlDbType.Int).Value = numeroB;
	                comand.Parameters.Add("@ubicacion", SqlDbType.NVarChar).Value = ubicacion;
                    comand.Parameters.Add("@piso", SqlDbType.Int).Value = pisoB;

                    comand.ExecuteNonQuery();

                }
            }

        }


    }
}
