using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus;
using FrbaBus.Abm_Rol;

namespace FrbaBus.Abm_Recorrido
{
    public partial class ABM_Recorrido : Form1
    {
        public ABM_Recorrido()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ( new AltaRecorrido() ).ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ( new BajaRecorrido() ).ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new ModifRecorrido()).ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new ListadoRol()).ShowDialog();
            this.Show();
        }
    }
}
