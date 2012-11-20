using System;
using System.Windows.Forms;
using WebDDF.Modelo;

namespace WebDDF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            variables.SelectedObject = lienzo.Diagrama;
            lienzo.Diagrama.PropertyChanged += (o, e) => { variables.Refresh(); };
        }

        private void agregarAsignación_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new Asignación());
        }

        private void agregarMientrasQue_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new CicloMientras());
        }

        private void agregarCicloPara_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new CicloPara());
        }

        private void agregarLectura_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new Lectura());
        }

        private void agregarSalida_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new Salida());
        }

        private void agregarDecisión_Click(object sender, EventArgs e)
        {
            lienzo.AgregarOperación(new Decisión());
        }

        private void lienzo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                lienzo.EliminarOperaciónSeleccionada();
            }
        }

        private void ejecutar_Click(object sender, EventArgs e)
        {
            lienzo.Ejecutar();
        }
    }
}
