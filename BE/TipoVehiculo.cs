using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE
{
    public class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public float ValorHora { get; set; }
        public float ValorEstadia { get; set; }
        public string Nombre { get; set; }

        public TipoVehiculo() { }

        public TipoVehiculo(int id)
        {
            IdTipoVehiculo = id;
        }

        public override string ToString()
        {
            return Nombre;
                
        }

    }
}
