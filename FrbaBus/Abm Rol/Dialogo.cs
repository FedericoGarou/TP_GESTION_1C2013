using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Abm_Rol
{
    public partial class Dialogo : Form
    {
        public Dialogo(String mensaje, String botonLeyenda)
        {
            InitializeComponent();
            label1.Text = mensaje;
            button1.Text = botonLeyenda;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
