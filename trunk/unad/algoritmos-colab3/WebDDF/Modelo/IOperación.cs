﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    interface IOperación
    {
        bool EstáSeleccionada { get; set; }
        Rectangle Rectángulo { get; set; }
        void Ejecutar(Diagrama diagrama);
        void Dibujar(Graphics g, ref Point centroArriba);
    }
}
