using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vehiculo
    {
      
        public int IdVehiculo { get; set; }
        public string patente { get; set; }
        public int abono {  get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
    }
}
