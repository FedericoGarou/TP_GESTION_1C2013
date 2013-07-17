using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaBus.Top_Micros;
using FrbaBus.Top_Destinos;
using FrbaBus.Top_Clientes;


namespace FrbaBus.Listado_Estadistico
{
    public partial class ListadoEstadistico : Form1
    {
        int semestre;
        string año;
        
        public ListadoEstadistico()
        {
            InitializeComponent();
            textBox1.Text = "Formato AAAA";
        }

        private void button5_Click(object sender, EventArgs e)
        {          
            try
            {
                this.sePuedeGenerarListado();
                año = textBox1.Text;

                if (checkBox1.Checked)
                {
                    semestre = 1;
                }
                else
                {
                    semestre = 2;
                }

                (new TopMicrosMayorPeriodoBaja(año,semestre)).Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeGenerarListado();
                año = textBox1.Text;

                if (checkBox1.Checked)
                {
                    semestre = 1;
                }
                else
                {
                    semestre = 2;
                }

                (new DestinosMasPasajesComprados(año, semestre)).Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }
        
        
        
        
        private void sePuedeGenerarListado()
        {
            String errorMensaje = "";
            bool hayError = false;

            if (textBox1.Text.Equals("Formato AAAA") || textBox1.Text.Length != 4 || Convert.ToInt64(textBox1.Text) < 2000)
            {
                hayError = true;
                errorMensaje += "Año no ingresado o en formato erroneo;";
            }

            if (!(checkBox1.Checked) && !(checkBox2.Checked))
            {
                hayError = true;
                errorMensaje += "Semestre no seleccionado;";
            }

            if ((checkBox1.Checked) && (checkBox2.Checked))
            {
                hayError = true;
                errorMensaje += "Mas de un semestre seleccionado;";
            }

            if (hayError)
                throw new Exception(errorMensaje);
        

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.sePuedeGenerarListado();
                año = textBox1.Text;

                if (checkBox1.Checked)
                {
                    semestre = 1;
                }
                else
                {
                    semestre = 2;
                }

                (new TopPuntos(año, semestre)).Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

       

            
    }
}
