using SolucionEjercicios.EjerciciosDfd;
using SolucionEjercicios.EjerciciosPseint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace SolucionEjercicios
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AsignarContenido(UserControl userControl)
        {
            userControl.Anchor = AnchorStyles.None;
            userControl.Left = (panelContenido.Width - userControl.Width) / 2;
            userControl.Top = (panelContenido.Height - userControl.Height) / 2;
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(userControl);
            MinimumSize = userControl.Size + new Size(10, 60);
        }

        private void calcularTriánguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new CalcularTriangulo());
        }

        private void aumentoAEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new AumentoEmpleados());
        }

        private void porcentajeDeHombresYMujeresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new HombresMujeres());
        }

        private void puntosParaExamenFinalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new PuntosExamen());
        }

        private void descuentoAVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new DescuentoVentas());
        }

        private void hipotenusaDeUnTriánguloRectánguloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new HipotenusaTrianguloRectangulo());
        }

        private void nominaDeAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new NominaAlmacen());
        }

        private void ingresoALaFiestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new IngresoFiesta());
        }

        private void sumaDePrimosYNoPrimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new SumaPrimosNoPrimos());
        }

        private void miércolesDeCenizaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarContenido(new MiercolesDeCeniza());
        }
    }
}
