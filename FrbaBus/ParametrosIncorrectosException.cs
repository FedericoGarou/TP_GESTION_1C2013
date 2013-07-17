using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrbaBus
{
    class ParametrosIncorrectosException : System.Exception
    {
        public ParametrosIncorrectosException(String mensaje) : base(mensaje) {}
    }
}
