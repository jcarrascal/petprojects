using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosDfd
{
    public partial class AumentoEmpleados : BaseEjercicio
    {
        public AumentoEmpleados()
        {
            InitializeComponent();
        }

        private void buttonIncrementar_Click(object sender, EventArgs e)
        {
            double sueldo1;
            if (!LeerNumeroPositivo(textBoxEmpleado1, out sueldo1))
                return;
            double sueldo2;
            if (!LeerNumeroPositivo(textBoxEmpleado2, out sueldo2))
                return;
            double sueldo3;
            if (!LeerNumeroPositivo(textBoxEmpleado3, out sueldo3))
                return;
            sueldo1 *= 1.10;
            sueldo2 *= 1.12;
            sueldo3 *= 1.15;
            if (sueldo1 <= sueldo2)
            {
                if (sueldo1 <= sueldo3)
                {
                    textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo1, sueldo2, sueldo3);
                }
                else if (sueldo1 < sueldo3)
                {
                    textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo1, sueldo3, sueldo2);
                }
                else
                {
                    textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo3, sueldo1, sueldo2);
                }
            }
            else if (sueldo1 <= sueldo3)
            {
                textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo2, sueldo1, sueldo3);
            }
            else if (sueldo2 <= sueldo3)
            {
                textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo2, sueldo3, sueldo1);
            }
            else
            {
                textBoxResultado.Text = string.Format("Los nuevos sueldos son {0}, {1}, {2}", sueldo3, sueldo2, sueldo1);
            }
        }
    }
}
