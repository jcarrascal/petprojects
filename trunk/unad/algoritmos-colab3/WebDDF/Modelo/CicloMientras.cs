using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WebDDF.Editores;

namespace WebDDF.Modelo
{
    class CicloMientras : IOperación, IPadreDeOperaciones
    {
        List<IOperación> operaciones = new List<IOperación>();

        public string Expresión { get; set; }

        public string Error { get; set; }

        public IPadreDeOperaciones Padre { get; set; }

        public Rectangle Rectángulo { get; set; }

        public bool Ejecutar(Diagrama diagrama)
        {
            string error;
            do 
            {
                object valor = diagrama.Evaluar(Expresión, out error);
                if (string.IsNullOrWhiteSpace(error))
                {
                    try
                    {
                        if (Convert.ToBoolean(valor))
                        {
                            foreach (IOperación operación in operaciones)
                            {
                                if (!operación.Ejecutar(diagrama))
                                    return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("El resultado de {{0}} no es un valor booleano. " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            while (true);
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            DibujarOperacion(g, ref centroArriba);
            foreach (IOperación operación in operaciones)
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
            using (Font font = new Font(SystemFonts.DefaultFont.FontFamily, 6))
                g.DrawString("MQ", font, Brushes.Black, new Point(r.X + reducción, r.Y + 6));
            Rectangle etiqueta = Rectangle.Inflate(Rectángulo, -Diagrama.OperaciónMárgen, -Diagrama.OperaciónMárgen);
            g.DrawString(Expresión, SystemFonts.DefaultFont, Brushes.Black, etiqueta, Diagrama.CentroMedio);
            centroArriba.Y += Rectángulo.Height;
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (CicloMientrasEditor editar = new CicloMientrasEditor(this))
                return editar.ShowDialog(parent);
        }

        Rectangle Medir(Point centroArriba)
        {
            int ancho = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + 100;
            int alto = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + Diagrama.OperaciónLinea;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
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
