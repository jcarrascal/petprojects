using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WebDDF.Editores;

namespace WebDDF.Modelo
{
    class Decisión : IOperación, IPadreDeOperaciones
    {
        List<IOperación> operacionesVerdadera = new List<IOperación>();

        List<IOperación> operacionesFalsa = new List<IOperación>();

        public string Expresión { get; set; }

        public string Error { get; set; }

        public IPadreDeOperaciones Padre { get; set; }

        public Rectangle Rectángulo { get; set; }

        public void Ejecutar(Diagrama diagrama)
        {
            throw new NotImplementedException();
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            DibujarOperacion(g, ref centroArriba);
            Point centroArribaDerecha = DibujarDerecha(g);
            Point centroArribaIzquierda = DibujarIzquierda(g);
            centroArriba.Y = Math.Max(centroArriba.Y, Math.Max(centroArribaDerecha.Y, centroArribaIzquierda.Y)) + 20;
            Point[] puntos = new Point[] {
                centroArribaDerecha,
                new Point(centroArribaDerecha.X, centroArriba.Y),
                new Point(centroArribaIzquierda.X, centroArriba.Y),
                centroArribaIzquierda,
            };
            g.DrawLines(Pens.Black, puntos);
        }

        private Point DibujarIzquierda(Graphics g)
        {
            Point bordeIzquierda = new Point(Rectángulo.X, Rectángulo.Y + Rectángulo.Height / 2);
            Point esquinaIzquierda = new Point(bordeIzquierda.X - 50, bordeIzquierda.Y);
            g.DrawLine(Pens.Black, bordeIzquierda, esquinaIzquierda);
            Point centroArriba = esquinaIzquierda;
            centroArriba.Y += Rectángulo.Height / 2;
            g.DrawLine(Pens.Black, esquinaIzquierda, centroArriba);
            foreach (IOperación operación in operacionesFalsa)
            {
                Diagrama.DibujarConector(g, ref centroArriba);
                operación.Dibujar(g, ref centroArriba);
            }
            return centroArriba;
        }

        private Point DibujarDerecha(Graphics g)
        {
            Point bordeDerecha = new Point(Rectángulo.X + Rectángulo.Width, Rectángulo.Y + Rectángulo.Height / 2);
            Point esquinaDerecha = new Point(bordeDerecha.X + 50, bordeDerecha.Y);
            g.DrawLine(Pens.Black, bordeDerecha, esquinaDerecha);
            Point centroArriba = esquinaDerecha;
            centroArriba.Y += Rectángulo.Height / 2;
            g.DrawLine(Pens.Black, esquinaDerecha, centroArriba);
            foreach (IOperación operación in operacionesVerdadera)
            {
                Diagrama.DibujarConector(g, ref centroArriba);
                operación.Dibujar(g, ref centroArriba);
            }
            return centroArriba;
        }

        private void DibujarOperacion(Graphics g, ref Point centroArriba)
        {
            Rectangle r = Rectángulo;
            Point[] puntos = new Point[] {
                new Point(r.X + r.Width / 2, r.Y),
                new Point(r.X + r.Width, r.Y + r.Height / 2),
                new Point(r.X + r.Width / 2, r.Y + r.Height),
                new Point(r.X, r.Y + r.Height / 2),
                new Point(r.X + r.Width / 2, r.Y),
            };
            g.FillPolygon(Brushes.White, puntos);
            g.DrawPolygon(Pens.Black, puntos);
            Rectangle etiqueta = Rectangle.Inflate(Rectángulo, -Diagrama.OperaciónMárgen, -Diagrama.OperaciónMárgen * 2);
            g.DrawString(Expresión, SystemFonts.DefaultFont, Brushes.Black, etiqueta, Diagrama.CentroMedio);
            using (Font font = new Font(SystemFonts.DefaultFont.FontFamily, 7))
            {
                g.DrawString("Sí", font, Brushes.Black, new Point(r.X + r.Width - 15, r.Y));
                g.DrawString("No", font, Brushes.Black, new Point(r.X, r.Y));
            }
            centroArriba.Y += Rectángulo.Height;
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (DecisiónEditor editar = new DecisiónEditor(this))
                return editar.ShowDialog(parent);
        }

        Rectangle Medir(Point centroArriba)
        {
            int ancho = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + 100;
            int alto = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + Diagrama.OperaciónLinea * 2;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
        }

        public IEnumerator<IOperación> GetEnumerator()
        {
            foreach (IOperación operación in operacionesVerdadera)
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
            operacionesVerdadera.Add(operación);
        }

        public void InsertarDespuésDe(IOperación operación, IOperación anterior)
        {
            Debug.Assert(anterior.Padre == this);
            operación.Padre = this;
            int indice = operacionesVerdadera.IndexOf(anterior);
            operacionesVerdadera.Insert(indice + 1, operación);
        }

        public void Eliminar(IOperación operación)
        {
            Debug.Assert(operación.Padre == this);
            operación.Padre = null;
            operacionesVerdadera.Remove(operación);
        }
    }
}
