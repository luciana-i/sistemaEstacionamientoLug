using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class VehiculoBL
    {
        public Vehiculo Obtener(int pId)
        {
            return VehiculoDAL.Obtener(pId);
        }

        public List<Vehiculo> Listar()
        {
            return VehiculoDAL.Listar();
        }

        public int Guardar(Vehiculo vehiculo)
        {
            return VehiculoDAL.Guardar(vehiculo);
        }

        public int Eliminar(int id)
        {
            return VehiculoDAL.Eliminar(id);
        }
    }
}
