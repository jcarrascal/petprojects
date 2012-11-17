namespace SolucionEjercicios.EjerciciosPseint
{
    partial class SumaPrimosNoPrimos
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
            this.dataGridViewNumeros = new System.Windows.Forms.DataGridView();
            this.numeroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSumaPrimos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSumaNoPrimos = new System.Windows.Forms.TextBox();
            this.valorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.esPrimoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumeros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeroBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewNumeros
            // 
            this.dataGridViewNumeros.AutoGenerateColumns = false;
            this.dataGridViewNumeros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNumeros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNumeros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.valorDataGridViewTextBoxColumn,
            this.esPrimoDataGridViewCheckBoxColumn});
            this.dataGridViewNumeros.DataSource = this.numeroBindingSource;
            this.dataGridViewNumeros.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewNumeros.Name = "dataGridViewNumeros";
            this.dataGridViewNumeros.Size = new System.Drawing.Size(391, 224);
            this.dataGridViewNumeros.TabIndex = 0;
            // 
            // numeroBindingSource
            // 
            this.numeroBindingSource.DataSource = typeof(SolucionEjercicios.EjerciciosPseint.Numero);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Suma de primos";
            // 
            // textBoxSumaPrimos
            // 
            this.textBoxSumaPrimos.Location = new System.Drawing.Point(6, 260);
            this.textBoxSumaPrimos.Name = "textBoxSumaPrimos";
            this.textBoxSumaPrimos.ReadOnly = true;
            this.textBoxSumaPrimos.Size = new System.Drawing.Size(192, 25);
            this.textBoxSumaPrimos.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Suma de los que no son primos";
            // 
            // textBoxSumaNoPrimos
            // 
            this.textBoxSumaNoPrimos.Location = new System.Drawing.Point(202, 260);
            this.textBoxSumaNoPrimos.Name = "textBoxSumaNoPrimos";
            this.textBoxSumaNoPrimos.ReadOnly = true;
            this.textBoxSumaNoPrimos.Size = new System.Drawing.Size(192, 25);
            this.textBoxSumaNoPrimos.TabIndex = 2;
            // 
            // valorDataGridViewTextBoxColumn
            // 
            this.valorDataGridViewTextBoxColumn.DataPropertyName = "Valor";
            this.valorDataGridViewTextBoxColumn.HeaderText = "Valor";
            this.valorDataGridViewTextBoxColumn.Name = "valorDataGridViewTextBoxColumn";
            // 
            // esPrimoDataGridViewCheckBoxColumn
            // 
            this.esPrimoDataGridViewCheckBoxColumn.DataPropertyName = "EsPrimo";
            this.esPrimoDataGridViewCheckBoxColumn.HeaderText = "Es Primo";
            this.esPrimoDataGridViewCheckBoxColumn.Name = "esPrimoDataGridViewCheckBoxColumn";
            this.esPrimoDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // SumaPrimosNoPrimos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxSumaNoPrimos);
            this.Controls.Add(this.textBoxSumaPrimos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewNumeros);
            this.Name = "SumaPrimosNoPrimos";
            this.Size = new System.Drawing.Size(402, 295);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumeros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeroBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewNumeros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSumaPrimos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSumaNoPrimos;
        private System.Windows.Forms.BindingSource numeroBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn esPrimoDataGridViewCheckBoxColumn;
    }
}
