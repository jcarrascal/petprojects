using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace SolucionEjercicios
{
    public partial class BaseEjercicio : UserControl
    {
        public BaseEjercicio()
        {
            InitializeComponent();
        }

        protected bool LeerNumeroPositivo(TextBox textBox, out double valor)
        {
            if (!double.TryParse(textBox.Text, out valor) || valor <= 0)
            {
                errorProvider.SetError(textBox, "Por favor ingrese un número.");
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }

        protected bool LeerNumeroPositivo(TextBox textBox, out int valor)
        {
            if (!int.TryParse(textBox.Text, out valor) || valor <= 0)
            {
                errorProvider.SetError(textBox, "Por favor ingrese un número.");
                return false;
            }
            errorProvider.SetError(textBox, string.Empty);
            return true;
        }
    }
}
