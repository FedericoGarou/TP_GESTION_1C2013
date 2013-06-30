namespace FrbaBus.Compra_de_Pasajes
{
    partial class EspecificarCompra
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ApePasajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombrePasajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoPasaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Butaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PisoButaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UbiButaca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.ApeDueño = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDueño = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KGPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(795, 240);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pasajes";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ApeDueño,
            this.NombreDueño,
            this.KGPaquete,
            this.Monto});
            this.dataGridView1.Location = new System.Drawing.Point(7, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(641, 207);
            this.dataGridView1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(654, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 65);
            this.button2.TabIndex = 1;
            this.button2.Text = "Eliminar encomienda";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(654, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "Agregar encomienda";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Location = new System.Drawing.Point(12, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pasajes";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ApePasajero,
            this.NombrePasajero,
            this.MontoPasaje,
            this.Butaca,
            this.PisoButaca,
            this.UbiButaca});
            this.dataGridView2.Location = new System.Drawing.Point(7, 19);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(641, 207);
            this.dataGridView2.TabIndex = 0;
            // 
            // ApePasajero
            // 
            this.ApePasajero.Frozen = true;
            this.ApePasajero.HeaderText = "Apellido pasajero";
            this.ApePasajero.Name = "ApePasajero";
            this.ApePasajero.ReadOnly = true;
            // 
            // NombrePasajero
            // 
            this.NombrePasajero.Frozen = true;
            this.NombrePasajero.HeaderText = "Nombre pasajero";
            this.NombrePasajero.Name = "NombrePasajero";
            this.NombrePasajero.ReadOnly = true;
            // 
            // MontoPasaje
            // 
            this.MontoPasaje.Frozen = true;
            this.MontoPasaje.HeaderText = "Monto";
            this.MontoPasaje.Name = "MontoPasaje";
            this.MontoPasaje.ReadOnly = true;
            // 
            // Butaca
            // 
            this.Butaca.Frozen = true;
            this.Butaca.HeaderText = "Numero butaca";
            this.Butaca.Name = "Butaca";
            this.Butaca.ReadOnly = true;
            // 
            // PisoButaca
            // 
            this.PisoButaca.Frozen = true;
            this.PisoButaca.HeaderText = "Piso butaca";
            this.PisoButaca.Name = "PisoButaca";
            this.PisoButaca.ReadOnly = true;
            // 
            // UbiButaca
            // 
            this.UbiButaca.Frozen = true;
            this.UbiButaca.HeaderText = "Ubicación butaca";
            this.UbiButaca.Name = "UbiButaca";
            this.UbiButaca.ReadOnly = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(654, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 65);
            this.button3.TabIndex = 1;
            this.button3.Text = "Eliminar Pasaje";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(654, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(137, 65);
            this.button4.TabIndex = 1;
            this.button4.Text = "Agregar Pasaje";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(12, 514);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(217, 45);
            this.button5.TabIndex = 1;
            this.button5.Text = "Cancelar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button1_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(586, 514);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(217, 45);
            this.button7.TabIndex = 1;
            this.button7.Text = "button1";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button1_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(590, 514);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(217, 45);
            this.button8.TabIndex = 1;
            this.button8.Text = "Continuar";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button1_Click);
            // 
            // ApeDueño
            // 
            this.ApeDueño.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ApeDueño.FillWeight = 150F;
            this.ApeDueño.Frozen = true;
            this.ApeDueño.HeaderText = "Apellido dueño";
            this.ApeDueño.Name = "ApeDueño";
            this.ApeDueño.ReadOnly = true;
            this.ApeDueño.Width = 150;
            // 
            // NombreDueño
            // 
            this.NombreDueño.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NombreDueño.FillWeight = 150F;
            this.NombreDueño.Frozen = true;
            this.NombreDueño.HeaderText = "Nombre dueño";
            this.NombreDueño.Name = "NombreDueño";
            this.NombreDueño.ReadOnly = true;
            this.NombreDueño.Width = 150;
            // 
            // KGPaquete
            // 
            this.KGPaquete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.KGPaquete.FillWeight = 150F;
            this.KGPaquete.Frozen = true;
            this.KGPaquete.HeaderText = "Kilos paquete";
            this.KGPaquete.Name = "KGPaquete";
            this.KGPaquete.ReadOnly = true;
            this.KGPaquete.Width = 150;
            // 
            // Monto
            // 
            this.Monto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Monto.FillWeight = 150F;
            this.Monto.Frozen = true;
            this.Monto.HeaderText = "Monto";
            this.Monto.Name = "Monto";
            this.Monto.ReadOnly = true;
            this.Monto.Width = 150;
            // 
            // EspecificarCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 567);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Name = "EspecificarCompra";
            this.Text = "EspecificarCompra";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApePasajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombrePasajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoPasaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Butaca;
        private System.Windows.Forms.DataGridViewTextBoxColumn PisoButaca;
        private System.Windows.Forms.DataGridViewTextBoxColumn UbiButaca;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApeDueño;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDueño;
        private System.Windows.Forms.DataGridViewTextBoxColumn KGPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
    }
}