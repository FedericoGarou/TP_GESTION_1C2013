namespace FrbaBus.Abm_Micro
{
    partial class ListadoMicro2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonFecHasta = new System.Windows.Forms.Button();
            this.buttonFecDesde = new System.Windows.Forms.Button();
            this.textFecHasta = new System.Windows.Forms.TextBox();
            this.textFecDesde = new System.Windows.Forms.TextBox();
            this.numericKGHasta = new System.Windows.Forms.NumericUpDown();
            this.numericKGDesde = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonFinal = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericKGHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKGDesde)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonFecHasta);
            this.groupBox2.Controls.Add(this.buttonFecDesde);
            this.groupBox2.Controls.Add(this.textFecHasta);
            this.groupBox2.Controls.Add(this.textFecDesde);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(364, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 125);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Que este libre en periodo...";
            // 
            // buttonFecHasta
            // 
            this.buttonFecHasta.Location = new System.Drawing.Point(10, 79);
            this.buttonFecHasta.Name = "buttonFecHasta";
            this.buttonFecHasta.Size = new System.Drawing.Size(212, 32);
            this.buttonFecHasta.TabIndex = 21;
            this.buttonFecHasta.Text = "Hasta";
            this.buttonFecHasta.UseVisualStyleBackColor = true;
            this.buttonFecHasta.Click += new System.EventHandler(this.buttonFecHasta_Click);
            // 
            // buttonFecDesde
            // 
            this.buttonFecDesde.Location = new System.Drawing.Point(10, 33);
            this.buttonFecDesde.Name = "buttonFecDesde";
            this.buttonFecDesde.Size = new System.Drawing.Size(212, 32);
            this.buttonFecDesde.TabIndex = 21;
            this.buttonFecDesde.Text = "Desde";
            this.buttonFecDesde.UseVisualStyleBackColor = true;
            this.buttonFecDesde.Click += new System.EventHandler(this.buttonFecDesde_Click);
            // 
            // textFecHasta
            // 
            this.textFecHasta.Enabled = false;
            this.textFecHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFecHasta.Location = new System.Drawing.Point(243, 82);
            this.textFecHasta.Name = "textFecHasta";
            this.textFecHasta.Size = new System.Drawing.Size(112, 24);
            this.textFecHasta.TabIndex = 15;
            // 
            // textFecDesde
            // 
            this.textFecDesde.Enabled = false;
            this.textFecDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFecDesde.Location = new System.Drawing.Point(243, 36);
            this.textFecDesde.Name = "textFecDesde";
            this.textFecDesde.Size = new System.Drawing.Size(112, 24);
            this.textFecDesde.TabIndex = 15;
            // 
            // numericKGHasta
            // 
            this.numericKGHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericKGHasta.Location = new System.Drawing.Point(177, 82);
            this.numericKGHasta.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericKGHasta.Name = "numericKGHasta";
            this.numericKGHasta.Size = new System.Drawing.Size(151, 26);
            this.numericKGHasta.TabIndex = 27;
            this.numericKGHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericKGHasta.ValueChanged += new System.EventHandler(this.numericKGHasta_ValueChanged);
            // 
            // numericKGDesde
            // 
            this.numericKGDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericKGDesde.Location = new System.Drawing.Point(177, 34);
            this.numericKGDesde.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericKGDesde.Name = "numericKGDesde";
            this.numericKGDesde.Size = new System.Drawing.Size(151, 26);
            this.numericKGDesde.TabIndex = 26;
            this.numericKGDesde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericKGDesde.ValueChanged += new System.EventHandler(this.numericKGDesde_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "KG Bodega hasta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "KG Bodega desde";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericKGDesde);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericKGHasta);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 125);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Con capacidad...";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(757, 167);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mas filtros";
            // 
            // buttonFinal
            // 
            this.buttonFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFinal.Location = new System.Drawing.Point(563, 481);
            this.buttonFinal.Name = "buttonFinal";
            this.buttonFinal.Size = new System.Drawing.Size(193, 46);
            this.buttonFinal.TabIndex = 33;
            this.buttonFinal.Text = "Buscar";
            this.buttonFinal.UseVisualStyleBackColor = true;
            this.buttonFinal.Click += new System.EventHandler(this.buttonFinal_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLimpiar.Location = new System.Drawing.Point(292, 481);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(193, 46);
            this.buttonLimpiar.TabIndex = 34;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.Location = new System.Drawing.Point(23, 481);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(193, 46);
            this.buttonCancelar.TabIndex = 32;
            this.buttonCancelar.Text = "Volver";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 197);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(733, 262);
            this.dataGridView1.TabIndex = 35;
            // 
            // ListadoMicro2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 539);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonFinal);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.groupBox3);
            this.Name = "ListadoMicro2";
            this.Text = "ListadoMicro2";
            this.Load += new System.EventHandler(this.ListadoMicro2_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericKGHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKGDesde)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFecHasta;
        private System.Windows.Forms.Button buttonFecDesde;
        private System.Windows.Forms.TextBox textFecHasta;
        private System.Windows.Forms.TextBox textFecDesde;
        private System.Windows.Forms.NumericUpDown numericKGHasta;
        private System.Windows.Forms.NumericUpDown numericKGDesde;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonFinal;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}