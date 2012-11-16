namespace SolucionEjercicios
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejerciciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calcularTriánguloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aumentoAEmpleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.porcentajeDeHombresYMujeresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ejerciciosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(728, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 19);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            // 
            // ejerciciosToolStripMenuItem
            // 
            this.ejerciciosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calcularTriánguloToolStripMenuItem,
            this.aumentoAEmpleadosToolStripMenuItem,
            this.porcentajeDeHombresYMujeresToolStripMenuItem});
            this.ejerciciosToolStripMenuItem.Name = "ejerciciosToolStripMenuItem";
            this.ejerciciosToolStripMenuItem.Size = new System.Drawing.Size(68, 19);
            this.ejerciciosToolStripMenuItem.Text = "Ejercicios";
            // 
            // calcularTriánguloToolStripMenuItem
            // 
            this.calcularTriánguloToolStripMenuItem.Name = "calcularTriánguloToolStripMenuItem";
            this.calcularTriánguloToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.calcularTriánguloToolStripMenuItem.Text = "01 Calcular triángulo";
            this.calcularTriánguloToolStripMenuItem.Click += new System.EventHandler(this.calcularTriánguloToolStripMenuItem_Click);
            // 
            // aumentoAEmpleadosToolStripMenuItem
            // 
            this.aumentoAEmpleadosToolStripMenuItem.Name = "aumentoAEmpleadosToolStripMenuItem";
            this.aumentoAEmpleadosToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.aumentoAEmpleadosToolStripMenuItem.Text = "02 Aumento a empleados";
            this.aumentoAEmpleadosToolStripMenuItem.Click += new System.EventHandler(this.aumentoAEmpleadosToolStripMenuItem_Click);
            // 
            // panelContenido
            // 
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 25);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(728, 512);
            this.panelContenido.TabIndex = 1;
            // 
            // porcentajeDeHombresYMujeresToolStripMenuItem
            // 
            this.porcentajeDeHombresYMujeresToolStripMenuItem.Name = "porcentajeDeHombresYMujeresToolStripMenuItem";
            this.porcentajeDeHombresYMujeresToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.porcentajeDeHombresYMujeresToolStripMenuItem.Text = "03 Porcentaje de hombres y mujeres";
            this.porcentajeDeHombresYMujeresToolStripMenuItem.Click += new System.EventHandler(this.porcentajeDeHombresYMujeresToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 537);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejerciciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calcularTriánguloToolStripMenuItem;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.ToolStripMenuItem aumentoAEmpleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porcentajeDeHombresYMujeresToolStripMenuItem;
    }
}

