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
    public partial class CalcularTriangulo : BaseEjercicio
    {
        public CalcularTriangulo()
        {
            InitializeComponent();
        }

        private void buttonCalcularEquilatero_Click(object sender, EventArgs e)
        {
            double lado;
            if (!LeerNumeroPositivo(textBoxLado, out lado))
                return;
            textBoxArea.Text = Convert.ToString(Math.Pow(lado, 2) * Math.Sqrt(3) / 4);
            textBoxPerimetro.Text = Convert.ToString(lado * 3);
        }

        private void buttonCalcularIsosceles_Click(object sender, EventArgs e)
        {
            double lados;
            if (!LeerNumeroPositivo(textBoxLados, out lados))
                return;
            double @base;
            if (!LeerNumeroPositivo(textBoxBase, out @base))
                return;
            double altura = Math.Sqrt(Math.Pow(lados, 2) - Math.Pow(@base / 2, 2));
            textBoxArea.Text = Convert.ToString((@base * altura) / 2);
            textBoxPerimetro.Text = Convert.ToString(lados * 2 + @base);
        }

        private void buttonCalcularEscaleno_Click(object sender, EventArgs e)
        {
            double ladoA;
            if (!LeerNumeroPositivo(textBoxLadoA, out ladoA))
                return;
            double ladoB;
            if (!LeerNumeroPositivo(textBoxLadoB, out ladoB))
                return;
            double ladoC;
            if (!LeerNumeroPositivo(textBoxLadoC, out ladoC))
                return;
            double perimetro = ladoA + ladoB + ladoC;
            double semiperimetro = perimetro / 2;
            double area = Math.Sqrt(semiperimetro * (semiperimetro - ladoA) * (semiperimetro - ladoB) * (semiperimetro - ladoC));
            textBoxArea.Text = Convert.ToString(area);
            textBoxPerimetro.Text = Convert.ToString(perimetro);
        }
    }
}
