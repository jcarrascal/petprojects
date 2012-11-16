using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolucionEjercicios.Ejercicios
{
    public partial class HipotenusaTrianguloRectangulo : BaseEjercicio
    {
        public HipotenusaTrianguloRectangulo()
        {
            InitializeComponent();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            double catetoA;
            if (!LeerNumeroPositivo(textBoxCatetoA, out catetoA))
                return;
            double catetoB;
            if (!LeerNumeroPositivo(textBoxCatetoB, out catetoB))
                return;
            double hipotenusa = Math.Sqrt(Math.Pow(catetoA, 2) + Math.Pow(catetoB, 2));
            textBoxResultado.Text = string.Format(
                "La hipotenusa del triángulo rectángulo con catetos {0} y {1} es de {2}.",
                catetoA,
                catetoB,
                hipotenusa);
        }
    }
}
