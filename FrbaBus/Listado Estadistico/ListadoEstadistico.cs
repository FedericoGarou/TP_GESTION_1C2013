using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Top_Micros;

namespace FrbaBus.Listado_Estadistico
{
    public partial class ListadoEstadistico : Form1
    {
        public ListadoEstadistico()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            (new TopMicrosMayorPeriodoBaja()).Show();
        }
    }
}
