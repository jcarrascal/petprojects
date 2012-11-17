namespace SolucionEjercicios.EjerciciosPseint
{
    partial class IngresoFiesta
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
            this.textBoxClave1 = new System.Windows.Forms.TextBox();
            this.textBoxClave2 = new System.Windows.Forms.TextBox();
            this.textBoxClave3 = new System.Windows.Forms.TextBox();
            this.textBoxClave4 = new System.Windows.Forms.TextBox();
            this.textBoxClave5 = new System.Windows.Forms.TextBox();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
            this.textBoxClave6 = new System.Windows.Forms.TextBox();
            this.buttonIngresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Claves de ingreso:";
            // 
            // textBoxClave1
            // 
            this.textBoxClave1.Location = new System.Drawing.Point(19, 24);
            this.textBoxClave1.Name = "textBoxClave1";
            this.textBoxClave1.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave1.TabIndex = 1;
            // 
            // textBoxClave2
            // 
            this.textBoxClave2.Location = new System.Drawing.Point(19, 55);
            this.textBoxClave2.Name = "textBoxClave2";
            this.textBoxClave2.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave2.TabIndex = 1;
            // 
            // textBoxClave3
            // 
            this.textBoxClave3.Location = new System.Drawing.Point(19, 86);
            this.textBoxClave3.Name = "textBoxClave3";
            this.textBoxClave3.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave3.TabIndex = 1;
            // 
            // textBoxClave4
            // 
            this.textBoxClave4.Location = new System.Drawing.Point(152, 24);
            this.textBoxClave4.Name = "textBoxClave4";
            this.textBoxClave4.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave4.TabIndex = 1;
            // 
            // textBoxClave5
            // 
            this.textBoxClave5.Location = new System.Drawing.Point(152, 55);
            this.textBoxClave5.Name = "textBoxClave5";
            this.textBoxClave5.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave5.TabIndex = 1;
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(19, 153);
            this.textBoxResultado.Multiline = true;
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(260, 63);
            this.textBoxResultado.TabIndex = 1;
            // 
            // textBoxClave6
            // 
            this.textBoxClave6.Location = new System.Drawing.Point(152, 86);
            this.textBoxClave6.Name = "textBoxClave6";
            this.textBoxClave6.Size = new System.Drawing.Size(127, 25);
            this.textBoxClave6.TabIndex = 1;
            // 
            // buttonIngresar
            // 
            this.buttonIngresar.Location = new System.Drawing.Point(152, 117);
            this.buttonIngresar.Name = "buttonIngresar";
            this.buttonIngresar.Size = new System.Drawing.Size(127, 30);
            this.buttonIngresar.TabIndex = 2;
            this.buttonIngresar.Text = "Ingresar";
            this.buttonIngresar.UseVisualStyleBackColor = true;
            this.buttonIngresar.Click += new System.EventHandler(this.buttonIngresar_Click);
            // 
            // IngresoFiesta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonIngresar);
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.textBoxClave6);
            this.Controls.Add(this.textBoxClave5);
            this.Controls.Add(this.textBoxClave4);
            this.Controls.Add(this.textBoxClave3);
            this.Controls.Add(this.textBoxClave2);
            this.Controls.Add(this.textBoxClave1);
            this.Controls.Add(this.label1);
            this.Name = "IngresoFiesta";
            this.Size = new System.Drawing.Size(297, 235);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxClave1;
        private System.Windows.Forms.TextBox textBoxClave2;
        private System.Windows.Forms.TextBox textBoxClave3;
        private System.Windows.Forms.TextBox textBoxClave4;
        private System.Windows.Forms.TextBox textBoxClave5;
        private System.Windows.Forms.TextBox textBoxResultado;
        private System.Windows.Forms.TextBox textBoxClave6;
        private System.Windows.Forms.Button buttonIngresar;
    }
}
