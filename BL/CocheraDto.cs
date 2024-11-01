using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CocheraDto
    {
        public int IdEspacio { get; set; }
        public int Piso { get; set; }
        public int PorcentajeValor { get; set; }
        public string Tamano { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public int IdCocheraFija { get; set; }
        public int IdCocheraMovil { get; set; }
        public int IdPlaya { get; set; }
        public Constantes.EstadosColeccion EstadoColeccion { get; set; }
        public int IndiceColeccion { get; set; }
        public Constantes.TipoCochera TipoCocheraEnum { get; set; }

        public string TipoCochera 
        { 
            get 
            { 
                if(Constantes.TipoCochera.Fija==TipoCocheraEnum)
                {
                    return "Cochera Fija";
                }
                else if (Constantes.TipoCochera.Movil == TipoCocheraEnum)
                {
                    return "Cochera Movil";
                }
                return "";
            } 
        }

    }
}
