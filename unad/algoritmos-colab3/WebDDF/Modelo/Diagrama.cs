using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WebDDF.Modelo
{
    class Diagrama : IPadreDeOperaciones
    {
        public const int OperaciónBorde = 1;
        public const int OperaciónMárgen = 3;
        public const int OperaciónLinea = 25;

        const int Márgen = 30;
        const int TamañoAlcance = 40;
        const int EspacioEntreOperaciones = 20;
        public static readonly StringFormat CentroMedio = new StringFormat(StringFormat.GenericDefault);

        static Diagrama()
        {
            CentroMedio.Trimming = StringTrimming.EllipsisCharacter;
            CentroMedio.FormatFlags = StringFormatFlags.NoWrap;
            CentroMedio.Alignment = StringAlignment.Center;
            CentroMedio.LineAlignment = StringAlignment.Center;
        }

        public readonly Dictionary<string, string> Variables = new Dictionary<string,string>();
        readonly List<IOperación> operaciones = new List<IOperación>();

        public void Dibujar(Graphics g, Rectangle espacio)
        {
            Point centroArriba = new Point(espacio.X + espacio.Width / 2, espacio.Y);
            DibujarAlcance("Inicio", g, ref centroArriba);
            foreach (IOperación operación in operaciones)
            {
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

        public static void DibujarAlcance(string etiqueta, Graphics g, ref Point centroArriba)
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

        public IEnumerator<IOperación> GetEnumerator()
        {
            foreach (IOperación operación in operaciones)
            {
                yield return operación;
                if (operación is IEnumerable<IOperación>)
                {
                    foreach (IOperación op in operación as IEnumerable<IOperación>)
                        yield return op;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Agregar(IOperación operación)
        {
            operación.Padre = this;
            operaciones.Add(operación);
        }

        public void InsertarDespuésDe(IOperación operación, IOperación anterior)
        {
            Debug.Assert(anterior.Padre == this);
            operación.Padre = this;
            int indice = operaciones.IndexOf(anterior);
            operaciones.Insert(indice + 1, operación);
        }

        public void Eliminar(IOperación operación)
        {
            Debug.Assert(operación.Padre == this);
            operación.Padre = null;
            operaciones.Remove(operación);
        }
    }
}
