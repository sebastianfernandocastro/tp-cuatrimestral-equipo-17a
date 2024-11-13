using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class FechaHora
    {
        public int Id{ get; set; }
        public DateTime Fecha { get; set; }     
        public TimeSpan Hora { get; set; }         
        public bool Disponible { get; set; }
    }
}
