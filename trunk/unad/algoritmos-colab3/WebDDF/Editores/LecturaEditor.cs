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
    partial class LecturaEditor : Form
    {
        readonly Lectura lectura;

        public LecturaEditor(Lectura lectura)
        {
            InitializeComponent();
            this.lectura = lectura;
            textBoxVariable.Text = lectura.Variable;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            lectura.Variable = textBoxVariable.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
