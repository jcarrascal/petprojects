﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Editores;
using WebDDF.Ejecución;

namespace WebDDF.Modelo
{
    class Lectura : IOperación
    {
        public string Variable { get; set; }

        public string Error { get; set; }

        public IPadreDeOperaciones Padre { get; set; }

        public Rectangle Rectángulo { get; set; }

        public bool Ejecutar(Diagrama diagrama)
        {
            if (string.IsNullOrWhiteSpace(Variable))
            {
                MessageBox.Show("El nombre de la variable no puede estar vacía", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            using (LecturaEjecución lecturaEjecución = new LecturaEjecución())
            {
                DialogResult dr = lecturaEjecución.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string error;
                    object valor = diagrama.Evaluar(lecturaEjecución.Resultado, out error);
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        diagrama[this.Variable] = valor;
                        return true;
                    }
                }
                return false;
            }
        }

        public void Dibujar(Graphics g, ref Point centroArriba)
        {
            Rectángulo = Medir(centroArriba);
            int reducción = 10;
            Rectangle r = Rectángulo;
            Point[] puntos = new Point[] {
                new Point(r.X, r.Y + reducción),
                new Point(r.X + r.Width, r.Y),
                new Point(r.X + r.Width, r.Y + r.Height),
                new Point(r.X, r.Y + r.Height),
            };
            g.FillPolygon(Brushes.White, puntos);
            g.DrawPolygon(Pens.Black, puntos);
            Rectangle etiqueta = Rectangle.Inflate(Rectángulo, -Diagrama.OperaciónMárgen, -Diagrama.OperaciónMárgen);
            g.DrawString(Variable, SystemFonts.DefaultFont, Brushes.Black, etiqueta, Diagrama.CentroMedio);
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
            using (LecturaEditor editar = new LecturaEditor(this))
                return editar.ShowDialog(parent);
        }
    }
}
