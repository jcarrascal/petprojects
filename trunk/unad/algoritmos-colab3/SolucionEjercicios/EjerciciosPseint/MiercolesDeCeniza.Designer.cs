namespace SolucionEjercicios.EjerciciosPseint
{
    partial class MiercolesDeCeniza
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
            this.textBoxAño = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMes = new System.Windows.Forms.TextBox();
            this.textBoxDía = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCalcular = new System.Windows.Forms.Button();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxAño
            // 
            this.textBoxAño.Location = new System.Drawing.Point(35, 56);
            this.textBoxAño.Name = "textBoxAño";
            this.textBoxAño.Size = new System.Drawing.Size(72, 25);
            this.textBoxAño.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "¿Cuál es la fecha del Domingo de Resurrección?";
            // 
            // textBoxMes
            // 
            this.textBoxMes.Location = new System.Drawing.Point(113, 56);
            this.textBoxMes.Name = "textBoxMes";
            this.textBoxMes.Size = new System.Drawing.Size(72, 25);
            this.textBoxMes.TabIndex = 1;
            // 
            // textBoxDía
            // 
            this.textBoxDía.Location = new System.Drawing.Point(191, 56);
            this.textBoxDía.Name = "textBoxDía";
            this.textBoxDía.Size = new System.Drawing.Size(72, 25);
            this.textBoxDía.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "AA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "MM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "DD";
            // 
            // buttonCalcular
            // 
            this.buttonCalcular.Location = new System.Drawing.Point(164, 98);
            this.buttonCalcular.Name = "buttonCalcular";
            this.buttonCalcular.Size = new System.Drawing.Size(102, 27);
            this.buttonCalcular.TabIndex = 3;
            this.buttonCalcular.Text = "Calcular";
            this.buttonCalcular.UseVisualStyleBackColor = true;
            this.buttonCalcular.Click += new System.EventHandler(this.buttonCalcular_Click);
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(42, 143);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(221, 70);
            this.textBoxResultado.TabIndex = 4;
            // 
            // MiercolesDeCeniza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.buttonCalcular);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDía);
            this.Controls.Add(this.textBoxMes);
            this.Controls.Add(this.textBoxAño);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MiercolesDeCeniza";
            this.Size = new System.Drawing.Size(309, 241);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAño;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMes;
        private System.Windows.Forms.TextBox textBoxDía;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCalcular;
        private System.Windows.Forms.TextBox textBoxResultado;
    }
}
