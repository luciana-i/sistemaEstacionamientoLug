using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Playa
    {
        public int IdPlaya { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
        public List<Espacio> ListaEspacios { get; set; }

        public Playa (int idPlaya)
        {
            IdPlaya = idPlaya;

        }

        public Playa()
        {

        }
    }
}
