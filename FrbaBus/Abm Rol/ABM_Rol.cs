using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus;

namespace FrbaBus.Abm_Rol
{
    public partial class ABM_Rol : Form1
    {
        public ABM_Rol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new AltaRol()).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new BajaRol()).Show();
        }       

        private void button3_Click(object sender, EventArgs e)
        {
            (new ModifRol()).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

       

               
    }
}
