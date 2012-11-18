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
        public readonly List<Variable> Variables = new List<Variable>();

        public Rectangle Rectángulo { get; set; }

        public void Ejecutar(Diagrama diagrama)
        {
            foreach (Variable variable in Variables)
            {
                string nombre = variable.Nombre;
                string expresión = variable.Expresión;
                diagrama.Variables[nombre] = diagrama.Evaluar(expresión);
            }
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            g.FillRectangle(Brushes.White, Rectángulo);
            g.DrawRectangle(Pens.Black, Rectángulo);
            centroArriba.Y += Rectángulo.Height;
            Rectangle etiqueta = new Rectangle(
                Rectángulo.X + Diagrama.OperaciónMárgen,
                Rectángulo.Y + Diagrama.OperaciónMárgen,
                Rectángulo.Width - (Diagrama.OperaciónMárgen * 2),
                Diagrama.OperaciónLinea);
            foreach (Variable variable in Variables)
            {
                g.DrawString(
                    variable.Nombre + " = " + variable.Expresión, 
                    SystemFonts.DefaultFont, 
                    Brushes.Black, 
                    etiqueta, 
                    Diagrama.CentroMedio);
                etiqueta.Y += Diagrama.OperaciónLinea;
            }
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (AsignaciónEditor editar = new AsignaciónEditor(this))
                return editar.ShowDialog(parent);
        }

        Rectangle Medir(Point centroArriba)
        {
            int ancho = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + 100;
            int alto = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + Math.Max(Variables.Count, 1) * Diagrama.OperaciónLinea;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
        }
    }

    public class Variable
    {
        public string Nombre { get; set; }

        public string Expresión { get; set; }
    }
}
