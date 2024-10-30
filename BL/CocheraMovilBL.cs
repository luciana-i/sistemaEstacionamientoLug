using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CocheraMovilBL : EspacioBL
    {
        public CocheraMovil Obtener(int pId)
        {
            return CocheraMovilDAL.Obtener(pId);
        }

        public List<CocheraMovil> Listar()
        {
            return CocheraMovilDAL.Listar();
        }

        public int Guardar(CocheraMovil cocheraMovil)
        {
            base.Guardar(cocheraMovil);
            cocheraMovil.IdEspacio = ((Espacio)cocheraMovil).IdEspacio;
            return CocheraMovilDAL.Guardar(cocheraMovil);
        }

        public int Eliminar(int id)
        {
            return CocheraMovilDAL.Eliminar(id);
        }
    }
}
