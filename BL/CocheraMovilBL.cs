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

        public List<CocheraMovil> ListarPorPlaya(int id)
        {
            return CocheraMovilDAL.ListarPorPlaya(id);
        }


        public int Guardar(CocheraMovil cocheraMovil)
        {
            base.Guardar(cocheraMovil);
            cocheraMovil.IdEspacio = ((Espacio)cocheraMovil).IdEspacio;
            return CocheraMovilDAL.Guardar(cocheraMovil);
        }

        public int Eliminar(CocheraMovil cochera)
        {
            CocheraMovilDAL.Eliminar(cochera.IdCocheraMovil);
            return EspacioDAL.Eliminar(cochera);
            

        }

        public static string SuperoCincoHoras(CocheraMovil cocheraMovil)
        {
            if (cocheraMovil != null)
            {
                cocheraMovil.HoraSalida = DateTime.Now.TimeOfDay;

                if ((cocheraMovil.HoraSalida - cocheraMovil.HoraEntrada).TotalHours >= 5)
                {
                   
                    return "Ha sobrepasado las 5 horas, deberá abonar estadía";
                }
            }
            return "";
        }

        public void EgresarAuto(CocheraMovil cochera)
        {
            cochera.HoraEntrada = TimeSpan.Zero;
            cochera.Vehiculo = null;
            Guardar(cochera);
        }

        public float ObtenerPrecioPorHora(Espacio espacio)
        {
            return (espacio.Vehiculo.TipoVehiculo.ValorHora - ((espacio.Vehiculo.TipoVehiculo.ValorHora * espacio.PorcentajeValor) / 100));
        }
    }
}
