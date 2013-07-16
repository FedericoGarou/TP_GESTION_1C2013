using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus.Cancelar_Viaje
{
    class FechaElegidaExeption : Exception
    {
        public FechaElegidaExeption(String mensaje) : base(mensaje) {}
    }
}
