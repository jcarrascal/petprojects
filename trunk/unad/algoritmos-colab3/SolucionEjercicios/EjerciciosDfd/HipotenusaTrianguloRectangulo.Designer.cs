namespace SolucionEjercicios.EjerciciosDfd
{
    partial class HipotenusaTrianguloRectangulo
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
            this.buttonCalcular = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
            this.textBoxCatetoB = new System.Windows.Forms.TextBox();
            this.textBoxCatetoA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCalcular
            // 
            this.buttonCalcular.Location = new System.Drawing.Point(123, 179);
            this.buttonCalcular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCalcular.Name = "buttonCalcular";
            this.buttonCalcular.Size = new System.Drawing.Size(120, 30);
            this.buttonCalcular.TabIndex = 22;
            this.buttonCalcular.Text = "Calcular";
            this.buttonCalcular.UseVisualStyleBackColor = true;
            this.buttonCalcular.Click += new System.EventHandler(this.buttonCalcular_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Longitud del cateto B";
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(5, 105);
            this.textBoxResultado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(238, 66);
            this.textBoxResultado.TabIndex = 19;
            // 
            // textBoxCatetoB
            // 
            this.textBoxCatetoB.Location = new System.Drawing.Point(5, 72);
            this.textBoxCatetoB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCatetoB.Name = "textBoxCatetoB";
            this.textBoxCatetoB.Size = new System.Drawing.Size(238, 25);
            this.textBoxCatetoB.TabIndex = 20;
            // 
            // textBoxCatetoA
            // 
            this.textBoxCatetoA.Location = new System.Drawing.Point(8, 22);
            this.textBoxCatetoA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCatetoA.Name = "textBoxCatetoA";
            this.textBoxCatetoA.Size = new System.Drawing.Size(238, 25);
            this.textBoxCatetoA.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Longitud del cateto A";
            // 
            // HipotenusaTrianguloRectangulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCalcular);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.textBoxCatetoA);
            this.Controls.Add(this.textBoxCatetoB);
            this.Name = "HipotenusaTrianguloRectangulo";
            this.Size = new System.Drawing.Size(273, 221);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalcular;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxResultado;
        private System.Windows.Forms.TextBox textBoxCatetoB;
        private System.Windows.Forms.TextBox textBoxCatetoA;
        private System.Windows.Forms.Label label1;
    }
}
