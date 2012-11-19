using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Editores;

namespace WebDDF.Modelo
{
    class Salida : IOperación
    {
        public string Expresión { get; set; }

        public IPadreDeOperaciones Padre { get; set; }

        public System.Drawing.Rectangle Rectángulo { get; set; }

        public void Ejecutar(Diagrama diagrama)
        {
            throw new NotImplementedException();
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            int reducción = 20;
            Rectangle r = Rectángulo;
            Point[] puntos = new Point[] {
                new Point(r.X, r.Y + r.Height / 2),
                new Point(r.X + r.Width - reducción, r.Y),

                new Point(r.X + r.Width, r.Y),
                new Point(r.X + r.Width, r.Y + r.Height),

                new Point(r.X + r.Width - reducción, r.Y + r.Height),
                new Point(r.X, r.Y + r.Height / 2),
            };
            byte[] tipos = new byte[] {
               (byte)PathPointType.Line,
               (byte)PathPointType.Line,
               (byte)PathPointType.Bezier,
               (byte)PathPointType.Bezier,
               (byte)PathPointType.Bezier,
               (byte)PathPointType.Line,
            };
            GraphicsPath path = new GraphicsPath(puntos, tipos);
            g.FillPath(Brushes.White, path);
            g.DrawPath(Pens.Black, path);
            Rectangle etiqueta = Rectangle.Inflate(Rectángulo, -Diagrama.OperaciónMárgen, -Diagrama.OperaciónMárgen);
            g.DrawString(Expresión, SystemFonts.DefaultFont, Brushes.Black, etiqueta, Diagrama.CentroMedio);
            centroArriba.Y += Rectángulo.Height;
        }

        private Rectangle Medir(Point centroArriba)
        {
            int ancho = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + 100;
            int alto = Diagrama.OperaciónBorde * 2 + Diagrama.OperaciónMárgen * 2 + Diagrama.OperaciónLinea;
            return new Rectangle(centroArriba.X - ancho / 2, centroArriba.Y, ancho, alto);
        }

        public DialogResult Editar(IWin32Window parent)
        {
            using (SalidaEditor editar = new SalidaEditor(this))
                return editar.ShowDialog(parent);
        }
    }
}
