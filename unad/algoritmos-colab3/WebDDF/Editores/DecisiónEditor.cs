using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Modelo;

namespace WebDDF.Editores
{
    partial class DecisiónEditor : Form
    {
        readonly Decisión decisión;

        public DecisiónEditor(Decisión decisión)
        {
            InitializeComponent();
            this.decisión = decisión;
            textBoxExpresión.Text = decisión.Expresión;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            decisión.Expresión = textBoxExpresión.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
