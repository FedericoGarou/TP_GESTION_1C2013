namespace FrbaBus.Abm_Micro
{
    partial class BajaMicro
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
            this.buttonBajaDefinitiva = new System.Windows.Forms.Button();
            this.buttonBajaMantenimiento = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBajaDefinitiva
            // 
            this.buttonBajaDefinitiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBajaDefinitiva.Location = new System.Drawing.Point(13, 13);
            this.buttonBajaDefinitiva.Name = "buttonBajaDefinitiva";
            this.buttonBajaDefinitiva.Size = new System.Drawing.Size(296, 43);
            this.buttonBajaDefinitiva.TabIndex = 0;
            this.buttonBajaDefinitiva.Text = "Dar de baja definitivamente";
            this.buttonBajaDefinitiva.UseVisualStyleBackColor = true;
            this.buttonBajaDefinitiva.Click += new System.EventHandler(this.buttonBajaDefinitiva_Click);
            // 
            // buttonBajaMantenimiento
            // 
            this.buttonBajaMantenimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBajaMantenimiento.Location = new System.Drawing.Point(13, 79);
            this.buttonBajaMantenimiento.Name = "buttonBajaMantenimiento";
            this.buttonBajaMantenimiento.Size = new System.Drawing.Size(296, 43);
            this.buttonBajaMantenimiento.TabIndex = 0;
            this.buttonBajaMantenimiento.Text = "Dar de baja por mantenimiento";
            this.buttonBajaMantenimiento.UseVisualStyleBackColor = true;
            this.buttonBajaMantenimiento.Click += new System.EventHandler(this.buttonBajaMantenimiento_Click);
            // 
            // BajaMicro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 139);
            this.Controls.Add(this.buttonBajaMantenimiento);
            this.Controls.Add(this.buttonBajaDefinitiva);
            this.Name = "BajaMicro";
            this.Text = "BajaMicro";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBajaDefinitiva;
        private System.Windows.Forms.Button buttonBajaMantenimiento;

    }
}