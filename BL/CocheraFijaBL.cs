using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CocheraFijaBL :EspacioBL
    {
        public CocheraFija Obtener(int pId)
        {
            return CocheraFijaDAL.Obtener(pId);
        }

    

        public List<CocheraFija> Listar()
        {
            return CocheraFijaDAL.Listar();
        }

        public List<CocheraFija> ListarPorPlaya(int id)
        {
            return CocheraFijaDAL.ListarPorPlaya(id);
        }

        public int Guardar(CocheraFija cocheraFija)
        {
            base.Guardar(cocheraFija);
            cocheraFija.IdEspacio = ((Espacio)cocheraFija).IdEspacio;  
            return CocheraFijaDAL.Guardar(cocheraFija);
        }

        public int Eliminar(CocheraFija cochera)
        {
            CocheraFijaDAL.Eliminar(cochera.IdCocheraFija);
            return EspacioDAL.Eliminar(cochera);
        }


   

    }
}
