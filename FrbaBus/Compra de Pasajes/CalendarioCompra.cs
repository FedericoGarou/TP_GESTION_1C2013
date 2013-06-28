using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaBus.Compra_de_Pasajes
{
    public partial class CalendarioCompra : Form1
    {
        private String fechaSalida;

        public CalendarioCompra()
        {
            InitializeComponent();
            monthCalendar1.TodayDate = this.getFechaActual();
            monthCalendar1.SelectionStart = this.getFechaActual();
            monthCalendar1.SelectionEnd = this.getFechaActual();
            fechaSalida = this.getFechaActual().ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new FechaElegidaExeption(fechaSalida);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            fechaSalida = monthCalendar1.SelectionEnd.ToShortDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
