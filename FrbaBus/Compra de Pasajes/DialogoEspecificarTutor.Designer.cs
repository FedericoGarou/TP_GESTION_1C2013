namespace FrbaBus.Compra_de_Pasajes
{
    partial class DialogoEspecificarTutor
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
            this.buttonExistente = new System.Windows.Forms.Button();
            this.buttonManual = new System.Windows.Forms.Button();
            this.dataGVExistentes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVExistentes)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExistente
            // 
            this.buttonExistente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExistente.Location = new System.Drawing.Point(24, 95);
            this.buttonExistente.Name = "buttonExistente";
            this.buttonExistente.Size = new System.Drawing.Size(576, 61);
            this.buttonExistente.TabIndex = 0;
            this.buttonExistente.Text = "Elegir tutor/acompañante entre los pasajes de la compra actual";
            this.buttonExistente.UseVisualStyleBackColor = true;
            this.buttonExistente.Click += new System.EventHandler(this.buttonExistente_Click);
            // 
            // buttonManual
            // 
            this.buttonManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonManual.Location = new System.Drawing.Point(24, 12);
            this.buttonManual.Name = "buttonManual";
            this.buttonManual.Size = new System.Drawing.Size(576, 61);
            this.buttonManual.TabIndex = 0;
            this.buttonManual.Text = "Ingresar tutor/acompañante manualmente";
            this.buttonManual.UseVisualStyleBackColor = true;
            this.buttonManual.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // dataGVExistentes
            // 
            this.dataGVExistentes.AllowUserToAddRows = false;
            this.dataGVExistentes.AllowUserToDeleteRows = false;
            this.dataGVExistentes.AllowUserToResizeColumns = false;
            this.dataGVExistentes.AllowUserToResizeRows = false;
            this.dataGVExistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVExistentes.Location = new System.Drawing.Point(24, 179);
            this.dataGVExistentes.Name = "dataGVExistentes";
            this.dataGVExistentes.Size = new System.Drawing.Size(576, 295);
            this.dataGVExistentes.TabIndex = 1;
            this.dataGVExistentes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVExistentes_CellContentClick);
            // 
            // DialogoEspecificarTutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 502);
            this.Controls.Add(this.dataGVExistentes);
            this.Controls.Add(this.buttonManual);
            this.Controls.Add(this.buttonExistente);
            this.Name = "DialogoEspecificarTutor";
            this.Text = "DialogoEspecificarTutor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGVExistentes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExistente;
        private System.Windows.Forms.Button buttonManual;
        private System.Windows.Forms.DataGridView dataGVExistentes;
    }
}