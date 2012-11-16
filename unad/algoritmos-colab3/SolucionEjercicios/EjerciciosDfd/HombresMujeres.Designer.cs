namespace SolucionEjercicios.EjerciciosDfd
{
    partial class HombresMujeres
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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxTotalMujeres = new System.Windows.Forms.TextBox();
            this.textBoxHombresCasados = new System.Windows.Forms.TextBox();
            this.textBoxTotalHombres = new System.Windows.Forms.TextBox();
            this.buttonCalcular = new System.Windows.Forms.Button();
            this.textBoxMujeresSolteras = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxResultado = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Total de mujeres:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Hombres casados";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Total de hombres:";
            // 
            // textBoxTotalMujeres
            // 
            this.textBoxTotalMujeres.Location = new System.Drawing.Point(6, 94);
            this.textBoxTotalMujeres.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTotalMujeres.Name = "textBoxTotalMujeres";
            this.textBoxTotalMujeres.Size = new System.Drawing.Size(131, 25);
            this.textBoxTotalMujeres.TabIndex = 9;
            // 
            // textBoxHombresCasados
            // 
            this.textBoxHombresCasados.Location = new System.Drawing.Point(162, 31);
            this.textBoxHombresCasados.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxHombresCasados.Name = "textBoxHombresCasados";
            this.textBoxHombresCasados.Size = new System.Drawing.Size(131, 25);
            this.textBoxHombresCasados.TabIndex = 8;
            // 
            // textBoxTotalHombres
            // 
            this.textBoxTotalHombres.Location = new System.Drawing.Point(6, 31);
            this.textBoxTotalHombres.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTotalHombres.Name = "textBoxTotalHombres";
            this.textBoxTotalHombres.Size = new System.Drawing.Size(131, 25);
            this.textBoxTotalHombres.TabIndex = 7;
            // 
            // buttonCalcular
            // 
            this.buttonCalcular.Location = new System.Drawing.Point(173, 165);
            this.buttonCalcular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCalcular.Name = "buttonCalcular";
            this.buttonCalcular.Size = new System.Drawing.Size(120, 30);
            this.buttonCalcular.TabIndex = 10;
            this.buttonCalcular.Text = "Calcular";
            this.buttonCalcular.UseVisualStyleBackColor = true;
            this.buttonCalcular.Click += new System.EventHandler(this.buttonCalcular_Click);
            // 
            // textBoxMujeresSolteras
            // 
            this.textBoxMujeresSolteras.Location = new System.Drawing.Point(162, 94);
            this.textBoxMujeresSolteras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxMujeresSolteras.Name = "textBoxMujeresSolteras";
            this.textBoxMujeresSolteras.Size = new System.Drawing.Size(131, 25);
            this.textBoxMujeresSolteras.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Mujeres solteras";
            // 
            // textBoxResultado
            // 
            this.textBoxResultado.Location = new System.Drawing.Point(6, 127);
            this.textBoxResultado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxResultado.Name = "textBoxResultado";
            this.textBoxResultado.ReadOnly = true;
            this.textBoxResultado.Size = new System.Drawing.Size(287, 25);
            this.textBoxResultado.TabIndex = 9;
            // 
            // HombresMujeres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxMujeresSolteras);
            this.Controls.Add(this.textBoxResultado);
            this.Controls.Add(this.textBoxTotalMujeres);
            this.Controls.Add(this.textBoxHombresCasados);
            this.Controls.Add(this.textBoxTotalHombres);
            this.Controls.Add(this.buttonCalcular);
            this.Name = "HombresMujeres";
            this.Size = new System.Drawing.Size(315, 213);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxTotalMujeres;
        private System.Windows.Forms.TextBox textBoxHombresCasados;
        private System.Windows.Forms.TextBox textBoxTotalHombres;
        private System.Windows.Forms.Button buttonCalcular;
        private System.Windows.Forms.TextBox textBoxMujeresSolteras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxResultado;
    }
}
