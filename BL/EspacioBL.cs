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
            if(espacio.Vehiculo != null)
            {
                VehiculoBL vehiculoBl = new VehiculoBL();
                vehiculoBl.Guardar(espacio.Vehiculo);
            }         

            return EspacioDAL.Guardar(espacio);
        }

        public int Eliminar(Espacio espacio)
        {

            return EspacioDAL.Eliminar(espacio);
        }

        #region Metodos - Negocio
        public int PorcentajeValorChanged(int value)
        {
            int porcentajeInicial = 10;
            return value * porcentajeInicial;
        }

        public string DefinirTamanoDeEspacioParaTipoAuto(String tipoAuto)
        {
            switch (tipoAuto)
            {
                case "moto":
                    return "Chico";

                case "auto":
                    return "Mediano";

                case "camioneta":
                    return "Grande";
                default:
                    return "";
            }
        }

        public void BorrarVehiculo(Espacio espacio)
        {
            espacio.Vehiculo = null;
            Guardar(espacio);
        } 
        #endregion
    }
}
