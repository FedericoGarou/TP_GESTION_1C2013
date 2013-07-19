using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Abm_Micro
{
    public partial class BajaMicro : Form1
    {
        public BajaMicro()
        {
            InitializeComponent();
        }

        private void buttonBajaDefinitiva_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new BajaDefinitiva()).ShowDialog();
            this.Show();
        }

        private void buttonBajaMantenimiento_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new BajaFueraDeServicio()).ShowDialog();
            this.Show();
        }
    }
}
