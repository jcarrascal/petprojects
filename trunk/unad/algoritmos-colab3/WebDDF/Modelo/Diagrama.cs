using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    class Diagrama
    {
        const int Márgen = 30;
        const int TamañoAlcance = 40;
        const int EspacioEntreOperaciones = 20;
        public static readonly StringFormat CentroMedio = new StringFormat(StringFormat.GenericDefault);

        static Diagrama()
        {
            CentroMedio.Alignment = StringAlignment.Center;
            CentroMedio.LineAlignment = StringAlignment.Center;
        }

        public readonly Dictionary<string, string> Variables = new Dictionary<string,string>();
        public readonly List<IOperación> Operaciones = new List<IOperación>();

        public void Dibujar(Graphics g, Rectangle espacio)
        {
            Point centroArriba = new Point(espacio.X + espacio.Width / 2, espacio.Y);
            DibujarAlcance("Inicio", g, ref centroArriba);
            foreach (IOperación operación in Operaciones)
            {
                // Dibujar flecha.
                DibujarConector(g, ref centroArriba);
                operación.Dibujar(g, ref centroArriba);
            }
            DibujarConector(g, ref centroArriba);
            DibujarAlcance("Fin", g, ref centroArriba);
        }

        public static void DibujarConector(Graphics g, ref Point centroArriba)
        {
            Point centroAbajo = centroArriba;
            centroArriba.Y += EspacioEntreOperaciones;
            g.DrawLine(Pens.Black, centroAbajo, centroArriba);
            Point[] flecha = new Point[] {
                centroArriba, 
                new Point(centroArriba.X - 3, centroArriba.Y - 6),
                new Point(centroArriba.X + 3, centroArriba.Y - 6),
            };
            g.FillPolygon(Brushes.Black, flecha, FillMode.Winding);
        }

        public void DibujarAlcance(string etiqueta, Graphics g, ref Point centroArriba)
        {
            Rectangle rectángulo = new Rectangle(centroArriba.X - TamañoAlcance / 2, centroArriba.Y, TamañoAlcance, TamañoAlcance);
            g.FillEllipse(Brushes.White, rectángulo);
            g.DrawEllipse(Pens.Black, rectángulo);
            g.DrawString(etiqueta, SystemFonts.DefaultFont, Brushes.Black, rectángulo, CentroMedio);
            centroArriba.Y += TamañoAlcance;
        }

        public string Evaluar(string expresión)
        {
            return expresión;
        }
    }
}
