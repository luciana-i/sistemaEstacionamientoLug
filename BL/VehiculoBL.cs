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

        public List<Vehiculo> ListarVehiculosSinEstacionar()
        {
            return VehiculoDAL.ListarVehiculosSinEstacionar();
        }

        public int Guardar(Vehiculo vehiculo)
        {
            return VehiculoDAL.Guardar(vehiculo);
        }

        public int Eliminar(int id)
        {
            
            return VehiculoDAL.Eliminar(id);
        }

        public bool ExisteUnVehiculoEnUso(int id)
        {
            EspacioBL espacioBL = new EspacioBL();
            return espacioBL.Listar().FirstOrDefault(x => x.Vehiculo != null && x.Vehiculo.IdVehiculo == id) != null;
        }
        
        
    }
}
