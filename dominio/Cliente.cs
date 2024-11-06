using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cliente : Usuario
    {
        public string DNI { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
    }
}
