using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class PlayaBL
    {
        public Playa Obtener(int pId)
        {
            return PlayaDAL.Obtener(pId);
        }

        public List<Playa> Listar()
        {
            return PlayaDAL.Listar();
        }

        public int Guardar(Playa playa)
        {
            return PlayaDAL.Guardar(playa);
        }

        public int Eliminar(int id)
        {
            return PlayaDAL.Eliminar(id);
        }
    }
}
