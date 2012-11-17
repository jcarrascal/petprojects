using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosPseint
{
    public partial class MiercolesDeCeniza : BaseEjercicio
    {
        public MiercolesDeCeniza()
        {
            InitializeComponent();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            int año;
            if (!LeerNumeroPositivo(textBoxAño, out año))
                return;
            bool esAñoBisiesto = EsAñoBisiesto(año);
            int mes;
            if (!LeerNumeroPositivo(textBoxMes, out mes))
                return;
            if (mes > 12)
            {
                errorProvider.SetError(textBoxMes, "Mes inválido");
                return;
            }
            int día;
            if (!LeerNumeroPositivo(textBoxDía, out día))
                return;
            if (día > DiasDelMes(mes, esAñoBisiesto))
            {
                errorProvider.SetError(textBoxDía, "Día inválido");
                return;
            }

            for (int i = 1; i <= 46; ++i)
            {
                --día;
                if (día < 1)
                {
                    --mes;
                    if (mes < 1)
                    {
                        día = 31;
                        mes = 12;
                        --año;
                        esAñoBisiesto = EsAñoBisiesto(año);
                    }
                    else
                    {
                        día = DiasDelMes(mes, esAñoBisiesto);
                    }
                }
            }
            textBoxResultado.Text = string.Format("La fecha para el miércoles de ceniza es {0}-{1}-{2}", año, mes, día);
        }

        private static bool EsAñoBisiesto(int año)
        {
            return (año % 4) == 0 && (año % 100) != 0;
        }

        private static int DiasDelMes(int mes, bool esAñoBisiesto)
        {
            int día;
            if (mes == 2)
            {
                if (esAñoBisiesto)
                    día = 29;
                else
                    día = 28;
            }
            else
            {
                día = 30;
                if (mes <= 7 && (mes % 2) != 0)
                    día = 31;
                if (mes >= 8 && (mes % 2) == 0)
                    día = 31;
            }
            return día;
        }
    }
}
