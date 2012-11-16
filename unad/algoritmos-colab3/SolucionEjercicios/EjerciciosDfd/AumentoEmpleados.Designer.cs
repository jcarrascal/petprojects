namespace SolucionEjercicios.EjerciciosDfd
{
    partial class AumentoEmpleados
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEmpleado1 = new System.Windows.Forms.TextBox();
            this.buttonIncrementar = new System.Windows.Forms.Button();
            this.textBoxEmpleado2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEmpleado3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Salario del empleado 1";
            // 
            // textBoxEmpleado1
            // 
            this.textBoxEmpleado1.Location = new System.Drawing.Point(166, 16);
            this.textBoxEmpleado1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEmpleado1.Name = "textBoxEmpleado1";
            this.textBoxEmpleado1.Size = new System.Drawing.Size(131, 25);
            this.textBoxEmpleado1.TabIndex = 0;
            // 
            // buttonIncrementar
            // 
            this.buttonIncrementar.Location = new System.Drawing.Point(166, 205);
            this.buttonIncrementar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonIncrementar.Name = "buttonIncrementar";
            this.buttonIncrementar.Size = new System.Drawing.Size(131, 30);
            this.buttonIncrementar.TabIndex = 3;
            this.buttonIncrementar.Text = "Incrementar";
            this.buttonIncrementar.UseVisualStyleBackColor = true;
            this.buttonIncrementar.Click += new System.EventHandler(this.buttonIncrementar_Click);
            // 
            // textBoxEmpleado2
            // 
            this.textBoxEmpleado2.Location = new System.Drawing.Point(166, 49);
            this.textBoxEmpleado2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEmpleado2.Name = "textBoxEmpleado2";
            this.textBoxEmpleado2.Size = new System.Drawing.Size(131, 25);
            this.textBoxEmpleado2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Salario del empleado 2";
            // 
            // textBoxEmpleado3
            // 
            this.textBoxEmpleado3.Location = new System.Drawing.Point(166, 82);
            this.textBoxEmpleado3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEmpleado3.Name = "textBoxEmpleado3";
            this.textBoxEmpleado3.Size = new System.Drawing.Size(131, 25);
            this.textBoxEmpleado3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Salario del empleado 3";
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(19, 122);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(278, 76);
            this.textBoxResultado.TabIndex = 4;
            // 
            // AumentoEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxEmpleado3);
            this.Controls.Add(this.textBoxEmpleado2);
            this.Controls.Add(this.textBoxEmpleado1);
            this.Controls.Add(this.buttonIncrementar);
            this.Name = "AumentoEmpleados";
            this.Size = new System.Drawing.Size(322, 252);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEmpleado1;
        private System.Windows.Forms.Button buttonIncrementar;
        private System.Windows.Forms.TextBox textBoxEmpleado2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEmpleado3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxResultado;
    }
}
