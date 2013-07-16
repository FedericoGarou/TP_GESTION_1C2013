namespace FrbaBus.Cancelar_Viaje
{
    partial class EspecificarCancelacion
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
            this.buttonContinuar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboCodEncomienda = new System.Windows.Forms.ComboBox();
            this.buttonAddEncomienda = new System.Windows.Forms.Button();
            this.buttonDelEncomienda = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboCodPasaje = new System.Windows.Forms.ComboBox();
            this.buttonAddPasaje = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textMotivo = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listListadoCodigos = new System.Windows.Forms.ListBox();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonContinuar
            // 
            this.buttonContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContinuar.Location = new System.Drawing.Point(12, 338);
            this.buttonContinuar.Name = "buttonContinuar";
            this.buttonContinuar.Size = new System.Drawing.Size(253, 45);
            this.buttonContinuar.TabIndex = 5;
            this.buttonContinuar.Text = "Finalizar";
            this.buttonContinuar.UseVisualStyleBackColor = true;
            this.buttonContinuar.Click += new System.EventHandler(this.buttonContinuar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboCodEncomienda);
            this.groupBox3.Controls.Add(this.buttonAddEncomienda);
            this.groupBox3.Location = new System.Drawing.Point(12, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 107);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Encomiendas";
            // 
            // comboCodEncomienda
            // 
            this.comboCodEncomienda.FormattingEnabled = true;
            this.comboCodEncomienda.Location = new System.Drawing.Point(23, 29);
            this.comboCodEncomienda.Name = "comboCodEncomienda";
            this.comboCodEncomienda.Size = new System.Drawing.Size(203, 21);
            this.comboCodEncomienda.TabIndex = 2;
            this.comboCodEncomienda.SelectedIndexChanged += new System.EventHandler(this.comboCodEncomienda_SelectedIndexChanged);
            // 
            // buttonAddEncomienda
            // 
            this.buttonAddEncomienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEncomienda.Location = new System.Drawing.Point(23, 56);
            this.buttonAddEncomienda.Name = "buttonAddEncomienda";
            this.buttonAddEncomienda.Size = new System.Drawing.Size(203, 30);
            this.buttonAddEncomienda.TabIndex = 1;
            this.buttonAddEncomienda.Text = "Agregar cancelación";
            this.buttonAddEncomienda.UseVisualStyleBackColor = true;
            this.buttonAddEncomienda.Click += new System.EventHandler(this.buttonAddEncomienda_Click);
            // 
            // buttonDelEncomienda
            // 
            this.buttonDelEncomienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelEncomienda.Location = new System.Drawing.Point(23, 215);
            this.buttonDelEncomienda.Name = "buttonDelEncomienda";
            this.buttonDelEncomienda.Size = new System.Drawing.Size(203, 30);
            this.buttonDelEncomienda.TabIndex = 1;
            this.buttonDelEncomienda.Text = "Eliminar cancelación";
            this.buttonDelEncomienda.UseVisualStyleBackColor = true;
            this.buttonDelEncomienda.Click += new System.EventHandler(this.buttonDelEncomienda_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.Location = new System.Drawing.Point(12, 398);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(253, 45);
            this.buttonCancelar.TabIndex = 4;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboCodPasaje);
            this.groupBox1.Controls.Add(this.buttonAddPasaje);
            this.groupBox1.Location = new System.Drawing.Point(12, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 110);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pasajes";
            // 
            // comboCodPasaje
            // 
            this.comboCodPasaje.FormattingEnabled = true;
            this.comboCodPasaje.Location = new System.Drawing.Point(23, 29);
            this.comboCodPasaje.Name = "comboCodPasaje";
            this.comboCodPasaje.Size = new System.Drawing.Size(203, 21);
            this.comboCodPasaje.TabIndex = 2;
            this.comboCodPasaje.SelectedIndexChanged += new System.EventHandler(this.comboCodPasaje_SelectedIndexChanged);
            // 
            // buttonAddPasaje
            // 
            this.buttonAddPasaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPasaje.Location = new System.Drawing.Point(23, 56);
            this.buttonAddPasaje.Name = "buttonAddPasaje";
            this.buttonAddPasaje.Size = new System.Drawing.Size(203, 30);
            this.buttonAddPasaje.TabIndex = 1;
            this.buttonAddPasaje.Text = "Agregar cancelación";
            this.buttonAddPasaje.UseVisualStyleBackColor = true;
            this.buttonAddPasaje.Click += new System.EventHandler(this.buttonAddPasaje_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textMotivo);
            this.groupBox2.Location = new System.Drawing.Point(283, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 139);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motivo";
            // 
            // textMotivo
            // 
            this.textMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMotivo.Location = new System.Drawing.Point(7, 20);
            this.textMotivo.Multiline = true;
            this.textMotivo.Name = "textMotivo";
            this.textMotivo.Size = new System.Drawing.Size(240, 102);
            this.textMotivo.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listListadoCodigos);
            this.groupBox4.Controls.Add(this.buttonDelEncomienda);
            this.groupBox4.Location = new System.Drawing.Point(283, 183);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 260);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pasajes/Encomiendas a cancelar";
            // 
            // listListadoCodigos
            // 
            this.listListadoCodigos.FormattingEnabled = true;
            this.listListadoCodigos.Location = new System.Drawing.Point(7, 20);
            this.listListadoCodigos.Name = "listListadoCodigos";
            this.listListadoCodigos.ScrollAlwaysVisible = true;
            this.listListadoCodigos.Size = new System.Drawing.Size(240, 186);
            this.listListadoCodigos.TabIndex = 0;
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLimpiar.Location = new System.Drawing.Point(12, 274);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(253, 45);
            this.buttonLimpiar.TabIndex = 5;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.buttonLimpiar_Click);
            // 
            // EspecificarCancelacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 471);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonContinuar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancelar);
            this.Name = "EspecificarCancelacion";
            this.Text = "EspecificarCancelacion";
            this.Load += new System.EventHandler(this.EspecificarCancelacion_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonContinuar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboCodEncomienda;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboCodPasaje;
        private System.Windows.Forms.Button buttonAddPasaje;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textMotivo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listListadoCodigos;
        private System.Windows.Forms.Button buttonDelEncomienda;
        private System.Windows.Forms.Button buttonAddEncomienda;
        private System.Windows.Forms.Button buttonLimpiar;

    }
}