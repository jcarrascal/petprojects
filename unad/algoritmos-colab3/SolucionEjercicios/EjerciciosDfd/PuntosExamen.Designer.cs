namespace SolucionEjercicios.EjerciciosDfd
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
            this.buttonCalcular = new System.Windows.Forms.Button();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
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
            this.textBoxPuntosActuales.Size = new System.Drawing.Size(238, 25);
            this.textBoxPuntosActuales.TabIndex = 12;
            // 
            // buttonCalcular
            // 
            this.buttonCalcular.Location = new System.Drawing.Point(124, 124);
            this.buttonCalcular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCalcular.Name = "buttonCalcular";
            this.buttonCalcular.Size = new System.Drawing.Size(120, 30);
            this.buttonCalcular.TabIndex = 14;
            this.buttonCalcular.Text = "Calcular";
            this.buttonCalcular.UseVisualStyleBackColor = true;
            this.buttonCalcular.Click += new System.EventHandler(this.buttonCalcular_Click);
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(6, 68);
            this.textBoxResultado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(238, 48);
            this.textBoxResultado.TabIndex = 12;
            // 
            // PuntosExamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCalcular);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.textBoxPuntosActuales);
            this.Name = "PuntosExamen";
            this.Size = new System.Drawing.Size(262, 158);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPuntosActuales;
        private System.Windows.Forms.Button buttonCalcular;
        private System.Windows.Forms.TextBox textBoxResultado;
    }
}
