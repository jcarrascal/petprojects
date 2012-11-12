using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDDF.Modelo
{
    class Diagrama
    {
        readonly Dictionary<string, string> Variables = new Dictionary<string,string>();
        readonly List<Operacion> Operaciones = new List<Operacion>();
    }
}
