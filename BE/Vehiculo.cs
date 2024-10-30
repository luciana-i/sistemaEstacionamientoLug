using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Vehiculo
    {
        public int IdVehiculo {  get; set; }
        public string Patente { get; set; }
        public string Abono { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }

        public Vehiculo() { }

        public Vehiculo(int id) 
        { 
            IdVehiculo = id;
        }
    }
}
