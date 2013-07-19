namespace FrbaBus.Abm_Micro
{
    partial class DialogoMicroFSTieneViajes
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReemplazar = new System.Windows.Forms.Button();
            this.buttonCancelarTodo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 154);
            this.label1.TabIndex = 0;
            this.label1.Text = "El micro al cual se le ha programado un mantenimiento tenía viajes programados en" +
                "tre esas fechas. ¿Que desea hacer?";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonReemplazar
            // 
            this.buttonReemplazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReemplazar.Location = new System.Drawing.Point(12, 157);
            this.buttonReemplazar.Name = "buttonReemplazar";
            this.buttonReemplazar.Size = new System.Drawing.Size(448, 51);
            this.buttonReemplazar.TabIndex = 1;
            this.buttonReemplazar.Text = "Reemplazar el micro si es posible";
            this.buttonReemplazar.UseVisualStyleBackColor = true;
            this.buttonReemplazar.Click += new System.EventHandler(this.buttonReemplazar_Click);
            // 
            // buttonCancelarTodo
            // 
            this.buttonCancelarTodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelarTodo.Location = new System.Drawing.Point(12, 224);
            this.buttonCancelarTodo.Name = "buttonCancelarTodo";
            this.buttonCancelarTodo.Size = new System.Drawing.Size(448, 51);
            this.buttonCancelarTodo.TabIndex = 1;
            this.buttonCancelarTodo.Text = "Cancelar todos los viajes afectados";
            this.buttonCancelarTodo.UseVisualStyleBackColor = true;
            this.buttonCancelarTodo.Click += new System.EventHandler(this.buttonCancelarTodo_Click);
            // 
            // DialogoMicroFSTieneViajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 305);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancelarTodo);
            this.Controls.Add(this.buttonReemplazar);
            this.Controls.Add(this.label1);
            this.Name = "DialogoMicroFSTieneViajes";
            this.Text = "DialogoMicroFSTieneViajes";
            this.Load += new System.EventHandler(this.DialogoMicroFSTieneViajes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReemplazar;
        private System.Windows.Forms.Button buttonCancelarTodo;
    }
}