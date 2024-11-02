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
            int res= PlayaDAL.Guardar(playa);
            GuardarEspacios(playa);

            return res;
        }

        private void GuardarEspacios(Playa playa)
        {
            object block = new object();
            bool acomodarIndices = false;
            lock (block) // esto previene la mutua exclusion 
            {
                CocheraFijaBL cocheraFijaBL = new CocheraFijaBL();
                CocheraMovilBL cocheraMovilBL = new CocheraMovilBL();
                for (int x = playa.ListaEspacios.Count - 1; x >= 0; x--)
                {
                    switch (playa.ListaEspacios[x].EstadoColeccion)
                    {
                        case Constantes.EstadosColeccion.Agregado:
                        case Constantes.EstadosColeccion.Modificado:
                            playa.ListaEspacios[x].IdPlaya = playa.IdPlaya;
                            if(playa.ListaEspacios[x] is CocheraFija)
                                cocheraFijaBL.Guardar((CocheraFija)playa.ListaEspacios[x]);
                            else
                                cocheraMovilBL.Guardar((CocheraMovil)playa.ListaEspacios[x]);
                            playa.ListaEspacios[x].EstadoColeccion = Constantes.EstadosColeccion.SinCambio;
                            break;
                        case Constantes.EstadosColeccion.Eliminado:
                            if (playa.ListaEspacios[x] is CocheraFija)
                                cocheraFijaBL.Eliminar((CocheraFija)playa.ListaEspacios[x]);
                            else
                                cocheraMovilBL.Eliminar((CocheraMovil)playa.ListaEspacios[x]);
                            playa.ListaEspacios.RemoveAt(playa.ListaEspacios[x].IndiceColeccion);
                            acomodarIndices = true;
                            break;
                        case Constantes.EstadosColeccion.Quitado:
                          //  playa.ListaEspacios.RemoveAt(playa.ListaEspacios[x].IndiceColeccion);
                            acomodarIndices = true;
                            break;
                    }
                }
            }
            if (acomodarIndices)
            {
                this.ReacomodarIndices(playa.ListaEspacios);
            }
        }

        private void ReacomodarIndices(List<Espacio> ListaEspacios)
        {
            for (int x = 0; x < ListaEspacios.Count; x++) { ListaEspacios[x].IndiceColeccion = x; }
        }

  
        public int Eliminar(int id)
        {
            return PlayaDAL.Eliminar(id);
        }

        public void EliminarEspacios(List<Espacio> ListaEspacios)
        {
            foreach (Espacio espacio in ListaEspacios)
            {
                EspacioDAL.Eliminar(espacio);
            }
        }
    }
}
