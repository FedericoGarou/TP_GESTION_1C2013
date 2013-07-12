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
            this.dataGVEncomienda = new System.Windows.Forms.DataGridView();
            this.buttonRemEncomienda = new System.Windows.Forms.Button();
            this.buttonAddEncomienda = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGVPasajes = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonAddPasaje = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonContinuar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVEncomienda)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPasajes)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGVEncomienda);
            this.groupBox3.Controls.Add(this.buttonRemEncomienda);
            this.groupBox3.Controls.Add(this.buttonAddEncomienda);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1057, 240);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Encomiendas";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // dataGVEncomienda
            // 
            this.dataGVEncomienda.AllowUserToAddRows = false;
            this.dataGVEncomienda.AllowUserToDeleteRows = false;
            this.dataGVEncomienda.AllowUserToResizeColumns = false;
            this.dataGVEncomienda.AllowUserToResizeRows = false;
            this.dataGVEncomienda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGVEncomienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVEncomienda.Location = new System.Drawing.Point(7, 19);
            this.dataGVEncomienda.Name = "dataGVEncomienda";
            this.dataGVEncomienda.Size = new System.Drawing.Size(871, 207);
            this.dataGVEncomienda.TabIndex = 0;
            // 
            // buttonRemEncomienda
            // 
            this.buttonRemEncomienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemEncomienda.Location = new System.Drawing.Point(899, 90);
            this.buttonRemEncomienda.Name = "buttonRemEncomienda";
            this.buttonRemEncomienda.Size = new System.Drawing.Size(137, 65);
            this.buttonRemEncomienda.TabIndex = 1;
            this.buttonRemEncomienda.Text = "Eliminar encomienda";
            this.buttonRemEncomienda.UseVisualStyleBackColor = true;
            this.buttonRemEncomienda.Click += new System.EventHandler(this.buttonRemEncomienda_Click);
            // 
            // buttonAddEncomienda
            // 
            this.buttonAddEncomienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEncomienda.Location = new System.Drawing.Point(899, 19);
            this.buttonAddEncomienda.Name = "buttonAddEncomienda";
            this.buttonAddEncomienda.Size = new System.Drawing.Size(137, 65);
            this.buttonAddEncomienda.TabIndex = 1;
            this.buttonAddEncomienda.Text = "Agregar encomienda";
            this.buttonAddEncomienda.UseVisualStyleBackColor = true;
            this.buttonAddEncomienda.Click += new System.EventHandler(this.buttonAddEncomienda_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGVPasajes);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.buttonAddPasaje);
            this.groupBox1.Location = new System.Drawing.Point(12, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1057, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pasajes";
            // 
            // dataGVPasajes
            // 
            this.dataGVPasajes.AllowUserToAddRows = false;
            this.dataGVPasajes.AllowUserToDeleteRows = false;
            this.dataGVPasajes.AllowUserToResizeColumns = false;
            this.dataGVPasajes.AllowUserToResizeRows = false;
            this.dataGVPasajes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGVPasajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVPasajes.Location = new System.Drawing.Point(7, 19);
            this.dataGVPasajes.Name = "dataGVPasajes";
            this.dataGVPasajes.Size = new System.Drawing.Size(871, 207);
            this.dataGVPasajes.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(899, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 65);
            this.button3.TabIndex = 1;
            this.button3.Text = "Eliminar Pasaje";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonAddPasaje
            // 
            this.buttonAddPasaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPasaje.Location = new System.Drawing.Point(899, 19);
            this.buttonAddPasaje.Name = "buttonAddPasaje";
            this.buttonAddPasaje.Size = new System.Drawing.Size(137, 65);
            this.buttonAddPasaje.TabIndex = 1;
            this.buttonAddPasaje.Text = "Agregar Pasaje";
            this.buttonAddPasaje.UseVisualStyleBackColor = true;
            this.buttonAddPasaje.Click += new System.EventHandler(this.buttonAddPasaje_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(328, 514);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(217, 45);
            this.button5.TabIndex = 1;
            this.button5.Text = "Cancelar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonContinuar
            // 
            this.buttonContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContinuar.Location = new System.Drawing.Point(590, 514);
            this.buttonContinuar.Name = "buttonContinuar";
            this.buttonContinuar.Size = new System.Drawing.Size(217, 45);
            this.buttonContinuar.TabIndex = 1;
            this.buttonContinuar.Text = "Continuar";
            this.buttonContinuar.UseVisualStyleBackColor = true;
            this.buttonContinuar.Click += new System.EventHandler(this.buttonContinuar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 526);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Precio total:";
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Enabled = false;
            this.textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotal.Location = new System.Drawing.Point(126, 523);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(180, 26);
            this.textBoxTotal.TabIndex = 3;
            this.textBoxTotal.Text = "0";
            this.textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EspecificarCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 567);
            this.Controls.Add(this.textBoxTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonContinuar);
            this.Controls.Add(this.button5);
            this.Name = "EspecificarCompra";
            this.Text = "EspecificarCompra";
            this.Load += new System.EventHandler(this.EspecificarCompra_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVEncomienda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVPasajes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGVEncomienda;
        private System.Windows.Forms.Button buttonAddEncomienda;
        private System.Windows.Forms.Button buttonRemEncomienda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGVPasajes;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonAddPasaje;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonContinuar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTotal;
    }
}