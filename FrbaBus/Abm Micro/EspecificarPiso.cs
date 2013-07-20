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
    public partial class EspecificarPiso : Form1
    {
        public int piso;

        public EspecificarPiso()
        {
            InitializeComponent();
        }

        private void EspecificarPiso_Load(object sender, EventArgs e)
        {
            comboPiso.Items.AddRange(new object[]{1,2});
            comboPiso.SelectedIndex = 0;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.piso = Convert.ToInt32(comboPiso.Text);
        }
    }
}
