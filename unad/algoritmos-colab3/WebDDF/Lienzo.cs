using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Modelo;

namespace WebDDF
{
    partial class Lienzo : UserControl
    {
        public Diagrama Diagrama { get; set; }

        public IOperación OperacionSeleccionada { get; set; }

        public Lienzo()
        {
            Diagrama = new Diagrama();

            InitializeComponent();
            this.DoubleBuffered = true;
            Paint += LienzoPaint;
            Resize += LienzoResize;
            MouseMove += LienzoMouseMove;
            MouseClick += LienzoMouseClick;
            MouseDoubleClick += LienzoMouseDoubleClick;
        }

        public void AddOperation(IOperación operación)
        {
            if (OperacionSeleccionada != null)
            {
                int indice = Diagrama.Operaciones.IndexOf(OperacionSeleccionada);
                Diagrama.Operaciones.Insert(indice + 1, operación);
            }
            else
                Diagrama.Operaciones.Add(operación);
            Invalidate();
        }

        void LienzoPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Diagrama.Dibujar(e.Graphics, e.ClipRectangle);
            if (OperacionSeleccionada != null)
            {
                Rectangle rectángulo = Rectangle.Inflate(OperacionSeleccionada.Rectángulo, 2, 2);
                e.Graphics.DrawRectangle(SystemPens.ActiveBorder, rectángulo);
            }
        }

        void LienzoResize(object sender, EventArgs e)
        {
            Invalidate();
        }

        void LienzoMouseMove(object sender, MouseEventArgs e)
        {
            Point mouse = new Point(e.X, e.Y);
            using (Graphics g = CreateGraphics())
            {
                foreach (IOperación operación in Diagrama.Operaciones)
                {
                    Rectangle rectángulo = Rectangle.Inflate(operación.Rectángulo, 2, 2);
                    if (rectángulo.Contains(mouse))
                        g.DrawRectangle(SystemPens.ActiveBorder, rectángulo);
                    else if (operación != OperacionSeleccionada)
                        g.DrawRectangle(SystemPens.ButtonFace, rectángulo);
                }
            }
        }

        void LienzoMouseClick(object sender, MouseEventArgs e)
        {
            OperacionSeleccionada = null;
            Point mouse = new Point(e.X, e.Y);
            foreach (IOperación operación in Diagrama.Operaciones)
            {
                if (operación.Rectángulo.Contains(mouse))
                    OperacionSeleccionada = operación;
            }
            Invalidate();
        }

        void LienzoMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point mouse = new Point(e.X, e.Y);
            foreach (IOperación operación in Diagrama.Operaciones)
            {
                if (operación.Rectángulo.Contains(mouse))
                {
                    if (operación.Editar(this) == DialogResult.OK)
                        Invalidate();
                    break;
                }
            }
        }
    }
}
