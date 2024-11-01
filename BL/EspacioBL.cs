using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BL
{
    public class EspacioBL
    {
        public Espacio Obtener(int pId)
        {
            return EspacioDAL.Obtener(pId);
        }

        

        public List<Espacio> Listar()
        {
            return EspacioDAL.Listar();
        }

        public int Guardar(Espacio espacio)
        {
            return EspacioDAL.Guardar(espacio);
        }

        public int Eliminar(int id)
        {
            return EspacioDAL.Eliminar(id);
        }
    }
}
