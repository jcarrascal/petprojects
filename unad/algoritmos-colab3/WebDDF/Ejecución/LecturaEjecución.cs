using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDDF.Ejecución
{
    public partial class LecturaEjecución : Form
    {
        public LecturaEjecución()
        {
            InitializeComponent();
        }

        public string Resultado
        {
            get { return textBoxResultado.Text; }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
