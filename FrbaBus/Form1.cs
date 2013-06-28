using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus
{
    public partial class Form1 : Form
    {
        private String stringDeConexion = "Server=localhost\\SQLSERVER2008;Database=GD1C2013;User Id=gd;Password=gd2013;";
        private DateTime fechaActual = new DateTime(2012,2,1); // Ajustar estos dos parametros por archivo de configuración

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public DateTime getFechaActual()
        {
            return this.fechaActual;
        }

        public void setFechaActual(String fecha)
        {
            this.fechaActual = Convert.ToDateTime(fecha);
        }

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(this.stringDeConexion);
        }

        public void cargarATablaParaDataGripView(string unaConsulta, ref DataTable unaTabla, SqlConnection unaConexion)
        {
            SqlCommand cmd = new SqlCommand(unaConsulta, unaConexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(unaTabla);
        }

        public DataGridViewButtonColumn crearBotones(String nombreColumna, String leyendaBoton)
        {
            DataGridViewButtonColumn botones = new DataGridViewButtonColumn();
            botones.HeaderText = nombreColumna;
            botones.Text = leyendaBoton;
            botones.UseColumnTextForButtonValue = true;
            botones.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            return botones;
        }
    }
}
