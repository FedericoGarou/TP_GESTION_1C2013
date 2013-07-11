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
    public partial class DialogoInsertarTutor : Form1
    {
        private int numeroVoucher;
        private int codigoViaje;
        private int DNI_Atutorado;
        
        public DialogoInsertarTutor(int numeroVoucher,int codigoViaje,int DNI)
        {
            InitializeComponent();
            this.numeroVoucher = numeroVoucher;
            this.codigoViaje = codigoViaje;
            this.DNI_Atutorado = DNI;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSi_Click(object sender, EventArgs e)
        {
            DialogoEspecificarTutor dialogoTutor = new DialogoEspecificarTutor(this.numeroVoucher,this.codigoViaje,this.DNI_Atutorado);
            dialogoTutor.ShowDialog();
            this.Close();
        }

        

    }
}
