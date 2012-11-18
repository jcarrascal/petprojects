using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Editores;

namespace WebDDF.Modelo
{
    class Asignación : IOperación
    {
        const int Borde = 1;
        const int Márgen = 3;
        const int Linea = 25;

        public readonly BindingList<KeyValuePair<string, string>> Expresiones = new BindingList<KeyValuePair<string, string>>();

        public Rectangle Rectángulo { get; set; }

        public void Ejecutar(Diagrama diagrama)
        {
            foreach (KeyValuePair<string, string> asignación in Expresiones)
            {
                string variable = asignación.Key;
                string expresión = asignación.Value;
                diagrama.Variables[variable] = diagrama.Evaluar(expresión);
            }
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            g.FillRectangle(Brushes.White, Rectángulo);
            g.DrawRectangle(Pens.Black, Rectángulo);
            centroArriba.Y += Rectángulo.Height;
            Rectangle etiqueta = new Rectangle(Rectángulo.X + Márgen, Rectángulo.Y + Márgen, Rectángulo.Width - (Márgen * 2), Linea);
            foreach (KeyValuePair<string, string> asignación in Expresiones)
            {
                g.DrawString(asignación.Key + " = " + asignación.Value, SystemFonts.DefaultFont, Brushes.Black, etiqueta, Diagrama.CentroMedio);
                etiqueta.Y += Linea;
            }
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (AsignacionEditor editar = new AsignacionEditor())
                return editar.ShowDialog(parent);
        }

        Rectangle Medir(Point centroArriba)
        {
            int ancho = Borde * 2 + Márgen * 2 + 100;
            int alto = Borde * 2 + Márgen * 2 + Math.Max(Expresiones.Count, 1) * Linea;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
        }
    }
}
