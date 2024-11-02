using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CocheraMovil :Espacio
    {
        public int IdCocheraMovil { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public new int IdEspacio { get; set; }

        public CocheraMovil() { }
        public CocheraMovil(int id)
        {
            IdCocheraMovil = id;
        }
    }
}
