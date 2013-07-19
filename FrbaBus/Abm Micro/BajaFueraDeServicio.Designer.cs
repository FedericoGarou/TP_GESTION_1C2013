namespace FrbaBus.Abm_Micro
{
    partial class BajaFueraDeServicio
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
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.comboPatentes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textFecInicio = new System.Windows.Forms.TextBox();
            this.buttonFecIni = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textFecFin = new System.Windows.Forms.TextBox();
            this.buttonFecFin = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAceptar.Location = new System.Drawing.Point(22, 407);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(319, 49);
            this.buttonAceptar.TabIndex = 5;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // comboPatentes
            // 
            this.comboPatentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPatentes.FormattingEnabled = true;
            this.comboPatentes.Location = new System.Drawing.Point(114, 6);
            this.comboPatentes.Name = "comboPatentes";
            this.comboPatentes.Size = new System.Drawing.Size(235, 28);
            this.comboPatentes.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Patente:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textFecInicio
            // 
            this.textFecInicio.Enabled = false;
            this.textFecInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFecInicio.Location = new System.Drawing.Point(6, 29);
            this.textFecInicio.Name = "textFecInicio";
            this.textFecInicio.Size = new System.Drawing.Size(319, 26);
            this.textFecInicio.TabIndex = 6;
            this.textFecInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFecIni
            // 
            this.buttonFecIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFecIni.Location = new System.Drawing.Point(6, 70);
            this.buttonFecIni.Name = "buttonFecIni";
            this.buttonFecIni.Size = new System.Drawing.Size(319, 39);
            this.buttonFecIni.TabIndex = 5;
            this.buttonFecIni.Text = "Seleccionar";
            this.buttonFecIni.UseVisualStyleBackColor = true;
            this.buttonFecIni.Click += new System.EventHandler(this.buttonFecIni_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textFecInicio);
            this.groupBox1.Controls.Add(this.buttonFecIni);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 124);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha inicio mantenimiento";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textFecFin);
            this.groupBox2.Controls.Add(this.buttonFecFin);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(16, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 124);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fecha final mantenimiento";
            // 
            // textFecFin
            // 
            this.textFecFin.Enabled = false;
            this.textFecFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFecFin.Location = new System.Drawing.Point(6, 29);
            this.textFecFin.Name = "textFecFin";
            this.textFecFin.Size = new System.Drawing.Size(319, 26);
            this.textFecFin.TabIndex = 6;
            this.textFecFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFecFin
            // 
            this.buttonFecFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFecFin.Location = new System.Drawing.Point(6, 70);
            this.buttonFecFin.Name = "buttonFecFin";
            this.buttonFecFin.Size = new System.Drawing.Size(319, 39);
            this.buttonFecFin.TabIndex = 5;
            this.buttonFecFin.Text = "Seleccionar";
            this.buttonFecFin.UseVisualStyleBackColor = true;
            this.buttonFecFin.Click += new System.EventHandler(this.buttonFecFin_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLimpiar.Location = new System.Drawing.Point(22, 343);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(319, 49);
            this.buttonLimpiar.TabIndex = 5;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // BajaFueraDeServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 468);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.comboPatentes);
            this.Controls.Add(this.label1);
            this.Name = "BajaFueraDeServicio";
            this.Text = "BajaFueraDeServicio";
            this.Load += new System.EventHandler(this.BajaFueraDeServicio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.ComboBox comboPatentes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFecInicio;
        private System.Windows.Forms.Button buttonFecIni;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textFecFin;
        private System.Windows.Forms.Button buttonFecFin;
        private System.Windows.Forms.Button buttonLimpiar;
    }
}