using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Abm_Recorrido
{
    public partial class VisualizarRecorrido : Form1
    {
        public VisualizarRecorrido( String Leyenda,
                                    String Origen,
                                    String Destino, 
                                    String Servicio, 
                                    decimal basePasaje, 
                                    decimal baseXKg)
        {
            InitializeComponent();

            label1.Text = Leyenda;

            listBox1.Height = listBox1.ItemHeight * 6;

            listBox1.Items.Add("Origen\t");
            listBox1.Items.Add("Destino\t");
            listBox1.Items.Add("Servicio\t");
            listBox1.Items.Add("Base pasaje\t");
            listBox1.Items.Add("Base por kilo\t");
            listBox1.Items.Add(Origen);
            listBox1.Items.Add(Destino);
            listBox1.Items.Add(Servicio);
            listBox1.Items.Add(basePasaje);
            listBox1.Items.Add(baseXKg);

            listBox1.MultiColumn = true;

        }

        public VisualizarRecorrido(String Leyenda,
                                    String Origen,
                                    String Destino,
                                    String Servicio,
                                    decimal basePasaje,
                                    decimal baseXKg,
                                    Boolean habilitado
                                    )
        {
            InitializeComponent();

            label1.Text = Leyenda;

            listBox1.Height = listBox1.ItemHeight * 7;

            listBox1.Items.Add("Origen\t");
            listBox1.Items.Add("Destino\t");
            listBox1.Items.Add("Servicio\t");
            listBox1.Items.Add("Base pasaje\t");
            listBox1.Items.Add("Base por kilo\t");
            listBox1.Items.Add("Habilitación\t");
            listBox1.Items.Add(Origen);
            listBox1.Items.Add(Destino);
            listBox1.Items.Add(Servicio);
            listBox1.Items.Add(basePasaje);
            listBox1.Items.Add(baseXKg);

            if(habilitado)
                listBox1.Items.Add("Habilitado");
            else
                listBox1.Items.Add("Deshabilitado");

            listBox1.MultiColumn = true;

        }

        public VisualizarRecorrido(String Leyenda,
                                    String Origen,
                                    String Destino,
                                    String Servicio) // Para bajas
        {
            InitializeComponent();

            label1.Text = Leyenda;

            listBox1.Height = listBox1.ItemHeight * 4;

            listBox1.Items.Add("Origen\t");
            listBox1.Items.Add("Destino\t");
            listBox1.Items.Add("Servicio\t");
            listBox1.Items.Add(Origen);
            listBox1.Items.Add(Destino);
            listBox1.Items.Add(Servicio);
            
            listBox1.MultiColumn = true;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
