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

        public string TipoCochera 
        { 
            get 
            { 
                if(IdCocheraFija != 0)
                {
                    return "Cochera Fija";
                }
                else if (IdCocheraMovil != 0)
                {
                    return "Cochera Movil";
                }
                return "";
            } 
        }

    }
}
