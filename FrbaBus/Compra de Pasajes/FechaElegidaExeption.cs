using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus.Compra_de_Pasajes
{
    class FechaElegidaExeption : Exception
    {
        public FechaElegidaExeption(String mensaje) : base(mensaje) {}
    }
}
