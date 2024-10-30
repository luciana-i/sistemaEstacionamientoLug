using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoVehiculoBL
    {
        public TipoVehiculo Obtener(int pId)
        {
            return TipoVehiculoDAL.Obtener(pId);
        }

        public List<TipoVehiculo> Listar()
        {
            return TipoVehiculoDAL.Listar();
        }

       
    }
}
