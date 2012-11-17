using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosDfd
{
    public partial class HombresMujeres : BaseEjercicio
    {
        public HombresMujeres()
        {
            InitializeComponent();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            int totalHombres;
            if (!LeerNumeroPositivo(textBoxTotalHombres, out totalHombres))
                return;
            int hombresCasados;
            if (!LeerNumeroPositivo(textBoxHombresCasados, out hombresCasados))
                return;
            if (hombresCasados > totalHombres)
            {
                errorProvider.SetError(textBoxHombresCasados, "No puede ser mayor al total");
                return;
            }

            int totalMujeres;
            if (!LeerNumeroPositivo(textBoxTotalMujeres, out totalMujeres))
                return;
            int mujeresSolteras;
            if (!LeerNumeroPositivo(textBoxMujeresSolteras, out mujeresSolteras))
                return;
            if (mujeresSolteras > totalMujeres)
            {
                errorProvider.SetError(textBoxMujeresSolteras, "No puede ser mayor al total");
                return;
            }

            float porcentajeHombresCasados = (hombresCasados * 100f) / totalHombres;
            float porcentajeMujeresSolteras = (mujeresSolteras * 100f) / totalMujeres;
            textBoxResultado.Text = string.Format("El porcentaje de hombres casados es {0}% y el porcentaje de mujeres solteras es {1}%", porcentajeHombresCasados, porcentajeMujeresSolteras);
        }
    }
}
