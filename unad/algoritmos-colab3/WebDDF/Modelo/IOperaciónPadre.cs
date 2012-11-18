using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    interface IOperaciónPadre : IOperación
    {
        List<IOperación> Operaciones { get; }
    }
}
