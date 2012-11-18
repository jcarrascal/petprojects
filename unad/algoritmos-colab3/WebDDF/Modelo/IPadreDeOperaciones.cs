using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    interface IPadreDeOperaciones : IEnumerable<IOperación>
    {
        void Agregar(IOperación operación);

        void InsertarDespuésDe(IOperación operación, IOperación anterior);

        void Eliminar(IOperación operación);
    }
}
