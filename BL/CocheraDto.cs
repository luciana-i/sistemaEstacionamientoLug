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
        #region Atributos
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
        public TimeSpan HoraEntrada { get; set; }

        public string Abono { get; set; }

        public string TipoCochera
        {
            get
            {
                if (Constantes.TipoCochera.Fija == TipoCocheraEnum)
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
        #endregion



        #region Metodos
        public Espacio TransformarCocheraDtoEnEspacio(CocheraDto cocheraDto)
        {
            Espacio cochera;
            if (cocheraDto.TipoCocheraEnum == Constantes.TipoCochera.Movil)
            {
                cochera = new CocheraMovil();
                (cochera as CocheraMovil).IdCocheraMovil = cocheraDto.IdCocheraMovil;

            }
            else
            {
                cochera = new CocheraFija();
                (cochera as CocheraFija).IdCocheraFija = cocheraDto.IdCocheraFija;
            }
            cochera.Piso = cocheraDto.Piso;
            cochera.Tamano = cocheraDto.Tamano;
            cochera.PorcentajeValor = cocheraDto.PorcentajeValor;
            cochera.IdEspacio = cocheraDto.IdEspacio;
            cochera.IdPlaya = cocheraDto.IdPlaya;
            cochera.IndiceColeccion = cocheraDto.IndiceColeccion;
            cochera.EstadoColeccion = cocheraDto.EstadoColeccion;

            return cochera;

        }

        public string CocheraOcupada()
        {
            return Vehiculo != null ? "Ocupada" : "Libre";
        }

        public string obtenerPatente()
        {
            return Vehiculo != null ? Vehiculo.Patente : "";
        }

        public string ObtenerAbono()
        {
            return Abono != null ? Abono : "";
        }
        #endregion
    }
}
