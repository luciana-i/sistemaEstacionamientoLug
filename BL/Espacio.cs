using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Espacio
    {
        public int Codigo { get; set; }
        public int Piso { get; set; }
        public int PorcentajeValor { get; set; }
        public int Tamano { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
