namespace SolucionEjercicios.EjerciciosPseint
{
    partial class NominaAlmacen
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewEmpleados = new System.Windows.Forms.DataGridView();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalVentasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salarioBaseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldoTotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTotalComisiones = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTotalVentas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTotalNomina = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empleadoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmpleados
            // 
            this.dataGridViewEmpleados.AutoGenerateColumns = false;
            this.dataGridViewEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreDataGridViewTextBoxColumn,
            this.totalVentasDataGridViewTextBoxColumn,
            this.salarioBaseDataGridViewTextBoxColumn,
            this.sueldoTotalDataGridViewTextBoxColumn});
            this.dataGridViewEmpleados.DataSource = this.empleadoBindingSource;
            this.dataGridViewEmpleados.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewEmpleados.Name = "dataGridViewEmpleados";
            this.dataGridViewEmpleados.Size = new System.Drawing.Size(508, 150);
            this.dataGridViewEmpleados.TabIndex = 0;
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Nombre";
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            this.nombreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalVentasDataGridViewTextBoxColumn
            // 
            this.totalVentasDataGridViewTextBoxColumn.DataPropertyName = "TotalVentas";
            this.totalVentasDataGridViewTextBoxColumn.HeaderText = "Total Ventas";
            this.totalVentasDataGridViewTextBoxColumn.Name = "totalVentasDataGridViewTextBoxColumn";
            // 
            // salarioBaseDataGridViewTextBoxColumn
            // 
            this.salarioBaseDataGridViewTextBoxColumn.DataPropertyName = "SalarioBase";
            this.salarioBaseDataGridViewTextBoxColumn.HeaderText = "Salario Base";
            this.salarioBaseDataGridViewTextBoxColumn.Name = "salarioBaseDataGridViewTextBoxColumn";
            // 
            // sueldoTotalDataGridViewTextBoxColumn
            // 
            this.sueldoTotalDataGridViewTextBoxColumn.DataPropertyName = "SueldoTotal";
            this.sueldoTotalDataGridViewTextBoxColumn.HeaderText = "Sueldo Total";
            this.sueldoTotalDataGridViewTextBoxColumn.Name = "sueldoTotalDataGridViewTextBoxColumn";
            this.sueldoTotalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // empleadoBindingSource
            // 
            this.empleadoBindingSource.DataSource = typeof(SolucionEjercicios.EjerciciosPseint.Empleado);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Por favor ingrese los empleados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total comisiones:";
            // 
            // textBoxTotalComisiones
            // 
            this.textBoxTotalComisiones.Location = new System.Drawing.Point(336, 211);
            this.textBoxTotalComisiones.Name = "textBoxTotalComisiones";
            this.textBoxTotalComisiones.ReadOnly = true;
            this.textBoxTotalComisiones.Size = new System.Drawing.Size(175, 25);
            this.textBoxTotalComisiones.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total ventas:";
            // 
            // textBoxTotalVentas
            // 
            this.textBoxTotalVentas.Location = new System.Drawing.Point(336, 242);
            this.textBoxTotalVentas.Name = "textBoxTotalVentas";
            this.textBoxTotalVentas.ReadOnly = true;
            this.textBoxTotalVentas.Size = new System.Drawing.Size(175, 25);
            this.textBoxTotalVentas.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Total nomina:";
            // 
            // textBoxTotalNomina
            // 
            this.textBoxTotalNomina.Location = new System.Drawing.Point(336, 273);
            this.textBoxTotalNomina.Name = "textBoxTotalNomina";
            this.textBoxTotalNomina.ReadOnly = true;
            this.textBoxTotalNomina.Size = new System.Drawing.Size(175, 25);
            this.textBoxTotalNomina.TabIndex = 3;
            // 
            // NominaAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxTotalNomina);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTotalVentas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTotalComisiones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewEmpleados);
            this.Name = "NominaAlmacen";
            this.Size = new System.Drawing.Size(514, 319);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empleadoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmpleados;
        private System.Windows.Forms.BindingSource empleadoBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTotalComisiones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTotalVentas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTotalNomina;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalVentasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn salarioBaseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldoTotalDataGridViewTextBoxColumn;
    }
}
