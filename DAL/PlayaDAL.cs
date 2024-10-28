using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class PlayaDAL
    {
         static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("Playa");
            id += 1;
            return id;
        }

        public static int Guardar(Playa playa)
        {
            if (playa.IdPlaya == 0)
            {
                playa.IdPlaya = ProximoId();
                string query = "INSERT INTO Playa (Id_Playa,Nombre, Direccion, hora_apertura, Hora_cierre) VALUES (" + playa.IdPlaya + ", '" + playa.Nombre + "', '" + playa.Direccion + "', CONVERT(TIME, '"+playa.HoraApertura+ "') , CONVERT(TIME, '" + playa.HoraCierre + "'))";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE Playa SET Direccion = '" + playa.Direccion + "', Nombre = '" + playa.Nombre + "', hora_apertura = CONVERT(TIME, '" + playa.HoraApertura + "') , Hora_cierre = CONVERT(TIME, '" + playa.HoraCierre + "') WHERE Id_Playa = " + playa.IdPlaya;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE Playa WHERE Id_Playa = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }

        public static Playa Obtener(int id)
        {
            string query = "SELECT * FROM Playa WHERE Id_Playa = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                Playa playa = new Playa(id);
                LlenarObjeto(playa, dset.Tables[0].Rows[0]);
                return playa;
            }
            else
            {
                return null;
            }

        }

        public static List<Playa> Listar()
        {
            string query = "SELECT * FROM Playa";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<Playa> listaPlayas = new List<Playa>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    Playa playa = new Playa(int.Parse(dr["id_Playa"].ToString()));
                    LlenarObjeto(playa, dr);
                    listaPlayas.Add(playa);
                }

            }
            return listaPlayas;
        }

        internal static void LlenarObjeto(Playa playa, DataRow dr)
        {
            playa.Direccion = dr["Direccion"].ToString();
            playa.Nombre = dr["Nombre"].ToString();
            playa.HoraApertura = TimeSpan.Parse(dr["hora_apertura"].ToString());
            playa.HoraCierre = TimeSpan.Parse(dr["Hora_cierre"].ToString());
        }

    }
}
