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
    public partial class MostrarButacasDisponibles : Form1
    {
        private int codigoViaje;
        public int pisoElegido;
        public int numeroBElegido;
        public String ubicacionElegida;

        public MostrarButacasDisponibles(int codigoViajeHeredado)
        {
            InitializeComponent();
            this.codigoViaje = codigoViajeHeredado;
            DataGridViewButtonColumn botonesComprarButaca = this.crearBotones("Comprar butaca", "Comprar");
            dataGridView1.Columns.Add(botonesComprarButaca);
            dataGridView1.Columns[0].Visible = false;
        }

        private void MostrarButacasDisponibles_Load(object sender, EventArgs e)
        {
            comboBoxPiso.Items.AddRange(new String[] { "Cualquier piso", "Primero", "Segundo" });
            comboBoxPiso.SelectedIndex = 0;
            comboBoxUbicacion.Items.AddRange(new String[] { "Cualquier ubicación", "Ventanilla", "Pasillo" });
            comboBoxUbicacion.SelectedIndex = 0;
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            comboBoxPiso.SelectedIndex = 0;
            comboBoxUbicacion.SelectedIndex = 0;
            dataGridView1.Columns[0].Visible = false;
        }

        private void buttonFiltrar_Click(object sender, EventArgs e)
        {
            String piso;

            if(comboBoxPiso.Text.Equals("Cualquier piso"))
                piso = "3";
            else
                piso = comboBoxPiso.SelectedIndex.ToString();

            using (SqlConnection conexion = this.obtenerConexion())
            { 
                String queryBuscadora = "SELECT * "+
                                        "FROM LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerButacasDisponibles("+
                                        this.codigoViaje + 
                                        ","+ piso +
                                        ",'"+ comboBoxUbicacion.Text +"')";
                
                using(SqlCommand comando = new SqlCommand(queryBuscadora,conexion))
                {
                    conexion.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    DataTable tablaButacasDisponiblesFiltradas = new DataTable();
                    adapter.Fill(tablaButacasDisponiblesFiltradas);
                    dataGridView1.DataSource = tablaButacasDisponiblesFiltradas;
                    dataGridView1.Columns[0].Visible = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.Columns[0].ReadOnly = false;
                }
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                this.numeroBElegido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                this.pisoElegido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                this.ubicacionElegida = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        


    }
}
