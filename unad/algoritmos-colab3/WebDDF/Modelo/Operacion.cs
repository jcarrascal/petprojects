using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    abstract class Operacion
    {
        public string Nombre;
        public abstract void Ejecutar();
    }
}
