using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaBus.Abm_Rol
{
    public partial class ListadoModificarRol : Form1
    {
        public ListadoModificarRol()
        {
            InitializeComponent();
            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    //cargar comboBox
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("USE GD1C2013 SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE (Nombre_Rol = 'Cliente') or (Nombre_Rol = 'Administrador')", conexion);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaDeNombres = new DataTable();

                    adapter.Fill(tablaDeNombres);

                    comboBox1.DisplayMember = "Nombre_Rol";
                    comboBox1.DataSource = tablaDeNombres;



                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        //boton buscar
        private void button2_Click(object sender, EventArgs e)
        {
            string varFiltro1;
            string varFiltro2;
            string varFiltro3;

            varFiltro1 = textBox1.Text;
            varFiltro2 = textBox2.Text;
            varFiltro3 = comboBox1.Text;

            using (SqlConnection conexion = this.obtenerConexion())
            {
                try
                {
                    conexion.Open();
                    DataTable tabla = new DataTable();

                    if (textBox1.Text.Length > 0)
                    {
                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol LIKE '%" + varFiltro1 + "%' and Habilitacion=1", ref tabla, conexion);

                        if ((textBox2.Text.Length) > 0 && (varFiltro2 != varFiltro3))
                        {
                            cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol = '" + varFiltro2 + "' and Nombre_Rol NOT LIKE '%" + varFiltro1 + "%' and Habilitacion=1", ref tabla, conexion);
                        }

                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol = '" + varFiltro3 + "' and Nombre_Rol NOT LIKE '%" + varFiltro1 + "%' and Habilitacion=1", ref tabla, conexion);

                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = tabla;

                        DataGridViewButtonColumn botonFuncionalidades = this.crearBoton("Funcionalidades", "Mostrar Funciondalidades");
                        dataGridView1.Columns.Add(botonFuncionalidades);
                        DataGridViewButtonColumn botonModificar = this.crearBoton("", "Modificar Rol");
                        dataGridView1.Columns.Add(botonModificar);
                    }
                    else
                    {

                        if ((textBox2.Text.Length) > 0 && (varFiltro2 != varFiltro3))
                        {
                            cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol = '" + varFiltro2 + "' and Habilitacion=1", ref tabla, conexion);
                        }

                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol = '" + varFiltro3 + "' and Habilitacion=1", ref tabla, conexion);

                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = tabla;

                        DataGridViewButtonColumn botonFuncionalidades = this.crearBoton("Funcionalidades", "Mostrar Funciondalidades");
                        dataGridView1.Columns.Add(botonFuncionalidades);
                        DataGridViewButtonColumn botonModificar = this.crearBoton("", "Modificar Rol");
                        dataGridView1.Columns.Add(botonModificar);
                    }
                }

                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
                }

            }
        }

        //boton limpiar
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            dataGridView1.DataSource = "";
            dataGridView1.Columns.Clear();
        }

        public DataGridViewButtonColumn crearBoton(String nombreColumna, String leyendaBoton)
        {
            DataGridViewButtonColumn botones = new DataGridViewButtonColumn();
            botones.HeaderText = nombreColumna;
            botones.Text = leyendaBoton;
            botones.UseColumnTextForButtonValue = true;
            botones.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            return botones;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    String nombreRol = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        conexion.Open();

                        String nombreRolActual = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                        if (e.ColumnIndex == 1) //boton mostrar funcionalidades
                        {


                            DataTable tabla = new DataTable();

                            cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_funcionalidad AS Funcionalidades FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + nombreRol + "'", ref tabla, conexion);
                            dataGridView2.Columns.Clear();
                            dataGridView2.DataSource = tabla;

                            DataGridViewButtonColumn botonModificar = this.crearBoton("", "Modificar / Eliminar / Agregar Funcionalidad");
                            dataGridView2.Columns.Add(botonModificar);

                        }

                        if (e.ColumnIndex == 2)//boton modificar rol
                        {

                            (new ModifRol(nombreRolActual)).Show();

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
                {
                    String nombreRolActual = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    String nombreFuncionalidadActual = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (e.ColumnIndex == 1) //boton modificar/agregar/elimar func
                    {
                        (new ModifRol(nombreRolActual, nombreFuncionalidadActual)).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                (new Dialogo("ERROR - " + ex.Message, "Aceptar")).ShowDialog();
            }
        }

    }
}