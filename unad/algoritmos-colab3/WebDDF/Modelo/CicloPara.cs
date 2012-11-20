using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Editores;

namespace WebDDF.Modelo
{
    class CicloPara : IOperación, IPadreDeOperaciones
    {
        List<IOperación> operaciones = new List<IOperación>();

        public CicloPara()
        {
            this.Paso = 1;
        }

        public string Variable { get; set; }

        public string Inicio { get; set; }

        public string Expresión { get; set; }

        public int Paso { get; set; }

        public string Error { get; set; }

        public IPadreDeOperaciones Padre { get; set; }

        public Rectangle Rectángulo { get; set; }

        public bool Ejecutar(Diagrama diagrama)
        {
            if (string.IsNullOrWhiteSpace(Variable))
            {
                MessageBox.Show("La variable no puede quedar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Inicio))
            {
                MessageBox.Show("El inicio no puede quedar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Expresión))
            {
                MessageBox.Show("La expresión no puede quedar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string error;
            int inicio;
            try
            {
                inicio = Convert.ToInt32(diagrama.Evaluar(Expresión, out error));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            diagrama[Variable] = inicio;
            int indice = inicio;
            do
            {
                object expresión = diagrama.Evaluar(Expresión, out error);
                if (string.IsNullOrWhiteSpace(error))
                {
                    try
                    {
                        if (Convert.ToBoolean(expresión))
                        {
                            foreach (IOperación operación in operaciones)
                            {
                                if (!operación.Ejecutar(diagrama))
                                    return false;
                            }
                            indice += Paso;
                            diagrama[Variable] = indice;
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
                g.DrawString("PARA", font, Brushes.Black, new Point(r.X + reducción, r.Y + 6));
            Rectangle etiqueta = Rectangle.Inflate(Rectángulo, -Diagrama.OperaciónMárgen, -Diagrama.OperaciónMárgen);
            g.DrawString(
                string.Format("{0} <- {1}, {2}, {3}", Variable, Inicio, Expresión, Paso),
                SystemFonts.DefaultFont, 
                Brushes.Black, 
                etiqueta, 
                Diagrama.CentroMedio);
            centroArriba.Y += Rectángulo.Height;
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (CicloParaEditor editar = new CicloParaEditor(this))
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
