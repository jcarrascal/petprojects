namespace SolucionEjercicios.Ejercicios
{
    partial class DescuentoVentas
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
            this.textBoxTotalCompra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCalcular
            // 
            this.buttonCalcular.Location = new System.Drawing.Point(121, 120);
            this.buttonCalcular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCalcular.Name = "buttonCalcular";
            this.buttonCalcular.Size = new System.Drawing.Size(120, 30);
            this.buttonCalcular.TabIndex = 18;
            this.buttonCalcular.Text = "Calcular";
            this.buttonCalcular.UseVisualStyleBackColor = true;
            this.buttonCalcular.Click += new System.EventHandler(this.buttonCalcular_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "\'¿Cuál es el total de la compra?\'";
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(3, 64);
            this.textBoxResultado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(238, 48);
            this.textBoxResultado.TabIndex = 15;
            // 
            // textBoxTotalCompra
            // 
            this.textBoxTotalCompra.Location = new System.Drawing.Point(3, 31);
            this.textBoxTotalCompra.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTotalCompra.Name = "textBoxTotalCompra";
            this.textBoxTotalCompra.Size = new System.Drawing.Size(238, 25);
            this.textBoxTotalCompra.TabIndex = 16;
            // 
            // DescuentoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCalcular);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.textBoxTotalCompra);
            this.Name = "DescuentoVentas";
            this.Size = new System.Drawing.Size(269, 167);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalcular;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxResultado;
        private System.Windows.Forms.TextBox textBoxTotalCompra;

    }
}
