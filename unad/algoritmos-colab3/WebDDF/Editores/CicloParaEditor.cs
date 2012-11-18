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
    partial class CicloParaEditor : Form
    {
        readonly CicloPara cicloPara;
        public CicloParaEditor(CicloPara cicloPara)
        {
            InitializeComponent();
            this.cicloPara = cicloPara;
            textBoxVariable.Text = cicloPara.Variable;
            textBoxInicio.Text = cicloPara.Inicio;
            textBoxExpresión.Text = cicloPara.Expresión;
            textBoxPaso.Text = cicloPara.Paso.ToString();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            int paso;
            if (!int.TryParse(textBoxPaso.Text, out paso) || paso == 0)
            {
                errorProvider.SetError(textBoxPaso, "No es un paso válido");
                return;
            }
            cicloPara.Variable = textBoxVariable.Text;
            cicloPara.Inicio = textBoxInicio.Text;
            cicloPara.Expresión = textBoxExpresión.Text;
            cicloPara.Paso = paso;
            DialogResult = DialogResult.OK;
        }
    }
}
