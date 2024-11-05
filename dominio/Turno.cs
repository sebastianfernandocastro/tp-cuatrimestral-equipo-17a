using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Turno
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public TipoVehiculo Vehiculo { get; set; }
        public Rubro Rubro { get; set; }
        public Servicio Servicio { get; set; }
        public string Aclaracion { get; set; }
        public FechaHora FechaHora { get; set; }
        public String Estado {  get; set; }
    }
}
