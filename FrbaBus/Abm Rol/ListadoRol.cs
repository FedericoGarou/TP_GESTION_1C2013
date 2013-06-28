﻿using System;
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
    public partial class ListadoRol : Form1
    {
        public ListadoRol()
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

                    comboBox1.Text = "";


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
            string textoFilto1;

            textoFilto1 = textBox1.Text;
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
                        varFiltro1 = "or Nombre_Rol LIKE '%" + textoFilto1 + "%'";
                    }
                    else
                    {
                        varFiltro1 = "";
                    }

                    cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_Rol, Habilitacion FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE Nombre_Rol = '" + varFiltro2 + "' or Nombre_Rol = '" + varFiltro3 + "'" + varFiltro1, ref tabla, conexion);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    DataGridViewButtonColumn botonFuncionalidades = this.crearBoton("Funcionalidades", "Mostrar Funciondalidades");
                    dataGridView1.Columns.Add(botonFuncionalidades);
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
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                String nombreRol = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (e.ColumnIndex == 2)
                {

                    using (SqlConnection conexion = this.obtenerConexion())
                    {
                        conexion.Open();
                        DataTable tabla = new DataTable();
                        
                        cargarATablaParaDataGripView("USE GD1C2013 SELECT Nombre_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol r join LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad rf on (r.Codigo_Rol = rf.Codigo_Rol) join LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad f on (rf.Codigo_Funcionalidad = f.Codigo_Funcionalidad) where Nombre_Rol = '" + nombreRol + "'", ref tabla, conexion);
                        
                        dataGridView2.DataSource = tabla;
                        dataGridView2.Columns[0].ReadOnly = true;
                    }

                }
            }


        }

    }
}