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
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public EstadoTurnos Estado { get; set; }
        public decimal Precio { get; set; }
    }
}
