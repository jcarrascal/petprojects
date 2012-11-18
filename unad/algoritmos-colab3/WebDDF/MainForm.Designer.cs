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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lienzo = new WebDDF.Lienzo();
            this.agregarAsignación = new System.Windows.Forms.ToolStripButton();
            this.agregarMientrasQue = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lienzo
            // 
            this.lienzo.Diagrama = diagrama1;
            this.lienzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lienzo.Location = new System.Drawing.Point(0, 25);
            this.lienzo.Name = "lienzo";
            this.lienzo.OperacionSeleccionada = null;
            this.lienzo.Size = new System.Drawing.Size(715, 367);
            this.lienzo.TabIndex = 1;
            // 
            // agregarAsignación
            // 
            this.agregarAsignación.Image = ((System.Drawing.Image)(resources.GetObject("agregarAsignación.Image")));
            this.agregarAsignación.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarAsignación.Name = "agregarAsignación";
            this.agregarAsignación.Size = new System.Drawing.Size(86, 22);
            this.agregarAsignación.Text = "Asignación";
            this.agregarAsignación.Click += new System.EventHandler(this.agregarAsignación_Click);
            // 
            // agregarMientrasQue
            // 
            this.agregarMientrasQue.Image = ((System.Drawing.Image)(resources.GetObject("agregarMientrasQue.Image")));
            this.agregarMientrasQue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.agregarMientrasQue.Name = "agregarMientrasQue";
            this.agregarMientrasQue.Size = new System.Drawing.Size(96, 22);
            this.agregarMientrasQue.Text = "Mientras que";
            this.agregarMientrasQue.Click += new System.EventHandler(this.agregarMientrasQue_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarAsignación,
            this.agregarMientrasQue});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 392);
            this.Controls.Add(this.lienzo);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
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
        private System.Windows.Forms.ToolStripButton agregarMientrasQue;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}

