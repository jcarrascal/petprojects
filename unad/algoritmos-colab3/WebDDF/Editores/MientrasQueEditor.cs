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
    partial class MientrasQueEditor : Form
    {
        readonly MientrasQue mientrasQue;

        public MientrasQueEditor(MientrasQue mientrasQue)
        {
            InitializeComponent();
            this.mientrasQue = mientrasQue;
            textBoxExpresión.Text = mientrasQue.Expresión;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            mientrasQue.Expresión = textBoxExpresión.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
