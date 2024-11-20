using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Precio
    {
        public int Id { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdRubro { get; set; }
        public int IdServicio { get; set; }
        public decimal PrecioValor { get; set; }

        public string TipoVehiculoNombre { get; set; }
        public string RubroNombre { get; set; }
        public string ServicioNombre { get; set; }
    }
}
