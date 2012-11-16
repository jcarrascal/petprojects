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
    public partial class DescuentoVentas : BaseEjercicio
    {
        public DescuentoVentas()
        {
            InitializeComponent();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            double totalCompra;
            if (!LeerNumeroPositivo(textBoxTotalCompra, out totalCompra))
                return;
            double descuento;
            if (totalCompra < 200000)
                descuento = totalCompra * 0.05;
            else
                descuento = totalCompra * 0.10;
            double precioFinal = totalCompra - descuento;
            textBoxResultado.Text = string.Format(
                "El descuento realizado es de ${0} y el precio final es ${1}", 
                descuento, 
                precioFinal);
        }
    }
}
