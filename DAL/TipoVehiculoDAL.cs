using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class TipoVehiculoDAL
    {
        static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("TipoVehiculo");
            id += 1;
            return id;
        }
        //** esto no se usaria porque no hay updates ni inserts ni tampoco deletes
        public static int Guardar(TipoVehiculo tipoVehiculo)
        {
            if (tipoVehiculo.IdTipoVehiculo == 0)
            {
                tipoVehiculo.IdTipoVehiculo = ProximoId();
                string query = "INSERT INTO TipoVehiculo (Id_Tipo_Vehiculo,Nombre, Valor_hora, Valor_Estadia) VALUES (" + tipoVehiculo.IdTipoVehiculo + ", '" + tipoVehiculo.Nombre + "', " + tipoVehiculo.ValorHora + ", " + tipoVehiculo.ValorEstadia+ ")";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE TipoVehiculo SET Nombre = '" + tipoVehiculo.Nombre + "', ValorHora = " + tipoVehiculo.ValorHora + ", ValorEstadia = " + tipoVehiculo.ValorEstadia + " WHERE Id_Tipo_Vehiculo = " + tipoVehiculo.IdTipoVehiculo;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE TipoVehiculo WHERE Id_Tipo_Vehiculo = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }
        /// al menos hasta aca
        public static TipoVehiculo Obtener(int id)
        {
            string query = "SELECT * FROM TipoVehiculo WHERE Id_Tipo_Vehiculo = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                TipoVehiculo tipoVehiculo = new TipoVehiculo(id);
                LlenarObjeto(tipoVehiculo, dset.Tables[0].Rows[0]);
                return tipoVehiculo;
            }
            else
            {
                return null;
            }

        }

        public static List<TipoVehiculo> Listar()
        {
            string query = "SELECT * FROM TipoVehiculo";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<TipoVehiculo> listaTipoVehiculos = new List<TipoVehiculo>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    TipoVehiculo tipoVehiculo = new TipoVehiculo(int.Parse(dr["Id_Tipo_Vehiculo"].ToString()));
                    LlenarObjeto(tipoVehiculo, dr);
                    listaTipoVehiculos.Add(tipoVehiculo);
                }

            }
            return listaTipoVehiculos;
        }

        internal static void LlenarObjeto(TipoVehiculo tipoVehiculo, DataRow dr)
        {
            tipoVehiculo.Nombre = dr["Nombre"].ToString();
            tipoVehiculo.ValorHora = float.Parse(dr["Valor_Hora"].ToString());
            tipoVehiculo.ValorEstadia = float.Parse(dr["Valor_Estadia"].ToString());
        }
    }
}
