using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL
{
    public class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public int ValorHora { get; set; }
        public int ValorEstadia { get; set; }   
        public string Nombre { get; set; }
    }
}
