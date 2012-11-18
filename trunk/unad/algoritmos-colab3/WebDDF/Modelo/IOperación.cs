using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDDF.Modelo
{
    interface IOperación
    {
        IPadreDeOperaciones Padre { get; set; }

        Rectangle Rectángulo { get; set; }

        void Ejecutar(Diagrama diagrama);

        void Dibujar(Graphics g, ref Point centroArriba);

        DialogResult Editar(IWin32Window parent);
    }
}
