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
    public partial class PuntosExamen : BaseEjercicio
    {
        public PuntosExamen()
        {
            InitializeComponent();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            int puntosActuales;
            if (!LeerNumeroPositivo(textBoxPuntosActuales, out puntosActuales))
                return;
            if (puntosActuales > 200)
            {
                errorProvider.SetError(textBoxPuntosActuales, "No puedes tener más de 200 puntos");
                return;
            }

            int puntosFaltantes = 300 - puntosActuales;
            textBoxResultado.Text = string.Format("Para aprobar la materia debes obtener mínimo {0} puntos", puntosFaltantes);
        }
    }
}
