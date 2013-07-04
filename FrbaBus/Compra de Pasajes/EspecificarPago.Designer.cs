namespace FrbaBus.Compra_de_Pasajes
{
    partial class EspecificarPago
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
            this.buttonEspecificarPagante = new System.Windows.Forms.Button();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxApellido = new System.Windows.Forms.TextBox();
            this.textBoxDNI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxPagoTarjeta = new System.Windows.Forms.GroupBox();
            this.numericNumTarjeta = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCuotas = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCompania = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTipoPago = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxPagoTarjeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNumTarjeta)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEspecificarPagante
            // 
            this.buttonEspecificarPagante.Location = new System.Drawing.Point(315, 21);
            this.buttonEspecificarPagante.Name = "buttonEspecificarPagante";
            this.buttonEspecificarPagante.Size = new System.Drawing.Size(199, 80);
            this.buttonEspecificarPagante.TabIndex = 2;
            this.buttonEspecificarPagante.Text = "Especificar datos de la persona que paga";
            this.buttonEspecificarPagante.UseVisualStyleBackColor = true;
            this.buttonEspecificarPagante.Click += new System.EventHandler(this.buttonEspecificarPagante_Click);
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Enabled = false;
            this.textBoxNombre.Location = new System.Drawing.Point(100, 77);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(209, 24);
            this.textBoxNombre.TabIndex = 1;
            this.textBoxNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxApellido
            // 
            this.textBoxApellido.Enabled = false;
            this.textBoxApellido.Location = new System.Drawing.Point(100, 49);
            this.textBoxApellido.Name = "textBoxApellido";
            this.textBoxApellido.Size = new System.Drawing.Size(209, 24);
            this.textBoxApellido.TabIndex = 1;
            this.textBoxApellido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDNI
            // 
            this.textBoxDNI.Enabled = false;
            this.textBoxDNI.Location = new System.Drawing.Point(100, 21);
            this.textBoxDNI.Name = "textBoxDNI";
            this.textBoxDNI.Size = new System.Drawing.Size(209, 24);
            this.textBoxDNI.TabIndex = 1;
            this.textBoxDNI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEspecificarPagante);
            this.groupBox1.Controls.Add(this.textBoxNombre);
            this.groupBox1.Controls.Add(this.textBoxApellido);
            this.groupBox1.Controls.Add(this.textBoxDNI);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 117);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Apellido:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNI:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(97, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tipo de pago";
            // 
            // groupBoxPagoTarjeta
            // 
            this.groupBoxPagoTarjeta.Controls.Add(this.numericNumTarjeta);
            this.groupBoxPagoTarjeta.Controls.Add(this.checkBoxCuotas);
            this.groupBoxPagoTarjeta.Controls.Add(this.label5);
            this.groupBoxPagoTarjeta.Controls.Add(this.textBoxCompania);
            this.groupBoxPagoTarjeta.Controls.Add(this.label7);
            this.groupBoxPagoTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPagoTarjeta.Location = new System.Drawing.Point(12, 185);
            this.groupBoxPagoTarjeta.Name = "groupBoxPagoTarjeta";
            this.groupBoxPagoTarjeta.Size = new System.Drawing.Size(528, 133);
            this.groupBoxPagoTarjeta.TabIndex = 1;
            this.groupBoxPagoTarjeta.TabStop = false;
            this.groupBoxPagoTarjeta.Text = "Datos del cliente";
            // 
            // numericNumTarjeta
            // 
            this.numericNumTarjeta.Enabled = false;
            this.numericNumTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericNumTarjeta.Location = new System.Drawing.Point(208, 37);
            this.numericNumTarjeta.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericNumTarjeta.Name = "numericNumTarjeta";
            this.numericNumTarjeta.Size = new System.Drawing.Size(209, 26);
            this.numericNumTarjeta.TabIndex = 3;
            this.numericNumTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxCuotas
            // 
            this.checkBoxCuotas.AutoSize = true;
            this.checkBoxCuotas.Enabled = false;
            this.checkBoxCuotas.Location = new System.Drawing.Point(153, 97);
            this.checkBoxCuotas.Name = "checkBoxCuotas";
            this.checkBoxCuotas.Size = new System.Drawing.Size(199, 22);
            this.checkBoxCuotas.TabIndex = 2;
            this.checkBoxCuotas.Text = "Acepta pago en cuotas";
            this.checkBoxCuotas.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Numero tarjeta:";
            // 
            // textBoxCompania
            // 
            this.textBoxCompania.Enabled = false;
            this.textBoxCompania.Location = new System.Drawing.Point(208, 67);
            this.textBoxCompania.Name = "textBoxCompania";
            this.textBoxCompania.Size = new System.Drawing.Size(209, 24);
            this.textBoxCompania.TabIndex = 1;
            this.textBoxCompania.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Companía:";
            // 
            // comboBoxTipoPago
            // 
            this.comboBoxTipoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoPago.FormattingEnabled = true;
            this.comboBoxTipoPago.Location = new System.Drawing.Point(233, 151);
            this.comboBoxTipoPago.Name = "comboBoxTipoPago";
            this.comboBoxTipoPago.Size = new System.Drawing.Size(196, 28);
            this.comboBoxTipoPago.TabIndex = 2;
            this.comboBoxTipoPago.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipoPago_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(12, 338);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(153, 44);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLimpiar.Location = new System.Drawing.Point(201, 338);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(153, 44);
            this.buttonLimpiar.TabIndex = 3;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConfirm.Location = new System.Drawing.Point(387, 338);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(153, 44);
            this.buttonConfirm.TabIndex = 3;
            this.buttonConfirm.Text = "Confirmar";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            // 
            // EspecificarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 397);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxTipoPago);
            this.Controls.Add(this.groupBoxPagoTarjeta);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Name = "EspecificarPago";
            this.Text = "EspecificarPago";
            this.Load += new System.EventHandler(this.EspecificarPago_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxPagoTarjeta.ResumeLayout(false);
            this.groupBoxPagoTarjeta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNumTarjeta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEspecificarPagante;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxApellido;
        private System.Windows.Forms.TextBox textBoxDNI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxPagoTarjeta;
        private System.Windows.Forms.TextBox textBoxCompania;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxCuotas;
        private System.Windows.Forms.ComboBox comboBoxTipoPago;
        private System.Windows.Forms.NumericUpDown numericNumTarjeta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.Button buttonConfirm;
    }
}