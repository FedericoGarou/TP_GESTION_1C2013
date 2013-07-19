using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus.Abm_Micro
{
    class FechaElegidaExeption : Exception
    {
        public FechaElegidaExeption(String mensaje) : base(mensaje) {}
    }
}
