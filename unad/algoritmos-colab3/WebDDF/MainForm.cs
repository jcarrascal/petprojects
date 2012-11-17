using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Modelo;

namespace WebDDF
{
    public partial class MainForm : Form
    {
        Diagrama diagrama;

        public MainForm()
        {
            InitializeComponent();
            var asignacion = new Asignación();
            asignacion.Expresiones.Add(new KeyValuePair<string, string>("Test", "13"));
            diagrama = new Diagrama();
            diagrama.Operaciones.Add(asignacion);
            diagrama.Operaciones.Add(new Asignación() { EstáSeleccionada = true });
        }

        private void panelLienzo_Paint(object sender, PaintEventArgs e)
        {
            diagrama.Dibujar(e.Graphics, e.ClipRectangle);
        }

        private void panelLienzo_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
