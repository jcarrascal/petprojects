using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDDF.Modelo
{
    class MientrasQue : IOperaciónPadre
    {
        readonly List<IOperación> operaciones = new List<IOperación>();

        public Rectangle Rectángulo { get; set; }

        public void Ejecutar(Diagrama diagrama)
        {
            throw new NotImplementedException();
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            DibujarOperacion(g, ref centroArriba);
            foreach (IOperación operación in Operaciones)
            {
                Diagrama.DibujarConector(g, ref centroArriba);
                operación.Dibujar(g, ref centroArriba);
            }
            Diagrama.DibujarConector(g, ref centroArriba);
            Diagrama.DibujarAlcance("Fin", g, ref centroArriba);
        }

        private void DibujarOperacion(Graphics g, ref Point centroArriba)
        {
            int reducción = 20;
            Rectangle r = Rectángulo;
            Point[] puntos = new Point[] {
                new Point(r.X, r.Y + r.Height / 2),
                new Point(r.X + reducción, r.Y),
                new Point(r.X + r.Width - reducción, r.Y),
                new Point(r.X + r.Width, r.Y + r.Height / 2),
                new Point(r.X + r.Width - reducción, r.Y + r.Height),
                new Point(r.X + reducción, r.Y + r.Height),
            };
            g.FillPolygon(Brushes.White, puntos);
            g.DrawPolygon(Pens.Black, puntos);
            centroArriba.Y += Rectángulo.Height;
        }

        public DialogResult Editar(IWin32Window parent)
        {
            throw new NotImplementedException();
        }

        public List<IOperación> Operaciones
        {
            get { return operaciones; }
        }

        Rectangle Medir(Point centroArriba)
        {
            int ancho = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + 100;
            int alto = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + Diagrama.OperaciónLinea;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
        }
    }
}
