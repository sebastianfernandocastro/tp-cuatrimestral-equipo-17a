using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Empleado : Usuario
    {
        public string legajo { get; set; }
        public int nivelAcceso { get; set; }
    }
}
