using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Espacio
    {
        public int IdEspacio { get; set; }
        public int Piso { get; set; }
        public int PorcentajeValor { get; set; }
        public string Tamano { get; set; }
        public Vehiculo Vehiculo { get; set; }

        public Espacio() { }
        public Espacio(int id)
        {
            IdEspacio = id;
        }

    }
}
