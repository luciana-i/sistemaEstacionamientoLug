using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Playa
    {
        public int IdPlaya { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int HoraApertura { get; set; }
        public int HoraCierre { get; set; }
        public List<Espacio> ListaEspacios { get; set; }
    }
}
