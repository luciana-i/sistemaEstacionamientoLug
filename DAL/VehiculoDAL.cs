using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VehiculoDAL
    {
        static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("Vehiculo");
            id += 1;
            return id;
        }

        public static int Guardar(Vehiculo vehiculo)
        {
            if (vehiculo.IdVehiculo == 0)
            {
                vehiculo.IdVehiculo = ProximoId();
                string query = "INSERT INTO Vehiculo (Id_Vehiculo,Patente, Abono, id_tipo_vehiculo) VALUES (" + vehiculo.IdVehiculo + ", '" + vehiculo.Patente + "', '" + vehiculo.Abono + "', " +vehiculo.TipoVehiculo.IdTipoVehiculo + ")";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE Vehiculo SET Patente = '" + vehiculo.Patente + "', Abono = '" + vehiculo.Abono + "', id_tipo_vehiculo = " + vehiculo.TipoVehiculo.IdTipoVehiculo +" WHERE Id_Vehiculo = " + vehiculo.IdVehiculo;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE Vehiculo WHERE Id_Vehiculo = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }

        public static Vehiculo Obtener(int id)
        {
            string query = "SELECT * FROM Vehiculo WHERE Id_Vehiculo = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                Vehiculo vehiculo = new Vehiculo(id);
                LlenarObjeto(vehiculo, dset.Tables[0].Rows[0]);
                return vehiculo;
            }
            else
            {
                return null;
            }

        }

        public static List<Vehiculo> Listar()
        {
            string query = "SELECT * FROM Vehiculo";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    Vehiculo vehiculo = new Vehiculo(int.Parse(dr["id_Vehiculo"].ToString()));
                    LlenarObjeto(vehiculo, dr);
                    listaVehiculos.Add(vehiculo);
                }

            }
            return listaVehiculos;
        }

        internal static void LlenarObjeto(Vehiculo vehiculo, DataRow dr)
        {
            vehiculo.Patente = dr["Patente"].ToString();
            vehiculo.Abono = dr["Abono"].ToString();

            TipoVehiculo tv = TipoVehiculoDAL.Obtener(int.Parse(dr["id_tipo_vehiculo"].ToString()));
            vehiculo.TipoVehiculo = tv;
        }
    }
}
