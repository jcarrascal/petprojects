namespace WebDDF
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
            WebDDF.Modelo.Diagrama diagrama1 = new WebDDF.Modelo.Diagrama();
            this.lienzo = new WebDDF.Lienzo();
            this.agregarAsignación = new System.Windows.Forms.ToolStripButton();
            this.agregarCicloMientras = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.agregarCicloPara = new System.Windows.Forms.ToolStripButton();
            this.agregarLectura = new System.Windows.Forms.ToolStripButton();
            this.agregarSalida = new System.Windows.Forms.ToolStripButton();
            this.agregarDecisión = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lienzo
            // 
            this.lienzo.Diagrama = diagrama1;
            this.lienzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lienzo.Location = new System.Drawing.Point(0, 25);
            this.lienzo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lienzo.Name = "lienzo";
            this.lienzo.OperacionSeleccionada = null;
            this.lienzo.Size = new System.Drawing.Size(624, 386);
            this.lienzo.TabIndex = 1;
            this.lienzo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lienzo_KeyUp);
            // 
            // agregarAsignación
            // 
            this.agregarAsignación.Image = global::WebDDF.Properties.Resources.Asignación;
            this.agregarAsignación.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarAsignación.Name = "agregarAsignación";
            this.agregarAsignación.Size = new System.Drawing.Size(86, 22);
            this.agregarAsignación.Text = "Asignación";
            this.agregarAsignación.Click += new System.EventHandler(this.agregarAsignación_Click);
            // 
            // agregarCicloMientras
            // 
            this.agregarCicloMientras.Image = global::WebDDF.Properties.Resources.CicloMientras;
            this.agregarCicloMientras.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarCicloMientras.Name = "agregarCicloMientras";
            this.agregarCicloMientras.Size = new System.Drawing.Size(103, 22);
            this.agregarCicloMientras.Text = "Ciclo mientras";
            this.agregarCicloMientras.Click += new System.EventHandler(this.agregarMientrasQue_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarAsignación,
            this.agregarCicloMientras,
            this.agregarCicloPara,
            this.agregarDecisión,
            this.agregarLectura,
            this.agregarSalida});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // agregarCicloPara
            // 
            this.agregarCicloPara.Image = global::WebDDF.Properties.Resources.CicloPara;
            this.agregarCicloPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarCicloPara.Name = "agregarCicloPara";
            this.agregarCicloPara.Size = new System.Drawing.Size(80, 22);
            this.agregarCicloPara.Text = "Ciclo para";
            this.agregarCicloPara.Click += new System.EventHandler(this.agregarCicloPara_Click);
            // 
            // agregarLectura
            // 
            this.agregarLectura.Image = global::WebDDF.Properties.Resources.Lectura;
            this.agregarLectura.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarLectura.Name = "agregarLectura";
            this.agregarLectura.Size = new System.Drawing.Size(66, 22);
            this.agregarLectura.Text = "Lectura";
            this.agregarLectura.Click += new System.EventHandler(this.agregarLectura_Click);
            // 
            // agregarSalida
            // 
            this.agregarSalida.Image = global::WebDDF.Properties.Resources.Salida;
            this.agregarSalida.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarSalida.Name = "agregarSalida";
            this.agregarSalida.Size = new System.Drawing.Size(58, 22);
            this.agregarSalida.Text = "Salida";
            this.agregarSalida.Click += new System.EventHandler(this.agregarSalida_Click);
            // 
            // agregarDecisión
            // 
            this.agregarDecisión.Image = global::WebDDF.Properties.Resources.Decisión;
            this.agregarDecisión.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarDecisión.Name = "agregarDecisión";
            this.agregarDecisión.Size = new System.Drawing.Size(72, 22);
            this.agregarDecisión.Text = "Decisión";
            this.agregarDecisión.Click += new System.EventHandler(this.agregarDecisión_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 411);
            this.Controls.Add(this.lienzo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "WebDDF";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Lienzo lienzo;
        private System.Windows.Forms.ToolStripButton agregarAsignación;
        private System.Windows.Forms.ToolStripButton agregarCicloMientras;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton agregarCicloPara;
        private System.Windows.Forms.ToolStripButton agregarLectura;
        private System.Windows.Forms.ToolStripButton agregarSalida;
        private System.Windows.Forms.ToolStripButton agregarDecisión;
    }
}

