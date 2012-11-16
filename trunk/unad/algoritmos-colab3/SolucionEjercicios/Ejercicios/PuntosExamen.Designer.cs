namespace SolucionEjercicios.Ejercicios
{
    partial class PuntosExamen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPuntosActuales = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "¿Cuántos puntos llevas?";
            // 
            // textBoxPuntosActuales
            // 
            this.textBoxPuntosActuales.Location = new System.Drawing.Point(6, 35);
            this.textBoxPuntosActuales.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPuntosActuales.Name = "textBoxPuntosActuales";
            this.textBoxPuntosActuales.Size = new System.Drawing.Size(131, 25);
            this.textBoxPuntosActuales.TabIndex = 12;
            // 
            // PuntosExamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPuntosActuales);
            this.Name = "PuntosExamen";
            this.Size = new System.Drawing.Size(317, 282);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPuntosActuales;
    }
}
