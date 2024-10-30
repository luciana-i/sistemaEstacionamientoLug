using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CocheraMovilDAL
    {
        static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("Cochera_Movil");
            id += 1;
            return id;
        }

        public static int Guardar(CocheraMovil cocheraMovil)
        {
            if (cocheraMovil.IdCocheraMovil == 0)
            {
                cocheraMovil.IdCocheraMovil = ProximoId();
                string query = "INSERT INTO Cochera_Movil (Id_cochera_Movil, hora_entrada, Hora_salida, Id_Espacio) VALUES (" + cocheraMovil.IdCocheraMovil + ", CONVERT(TIME, '" + cocheraMovil.HoraEntrada + "') , CONVERT(TIME, '" + cocheraMovil.HoraSalida + "'), "+ cocheraMovil.IdEspacio+")";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE Cochera_Movil SET hora_Entrada = CONVERT(TIME, '" + cocheraMovil.HoraEntrada + "') , Hora_salida = CONVERT(TIME, '" + cocheraMovil.HoraSalida + "') WHERE Id_cochera_Movil = " + cocheraMovil.IdCocheraMovil;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE Cochera_Movil WHERE Id_cochera_Movil = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }

        public static CocheraMovil Obtener(int id)
        {
            string query = "SELECT * FROM Cochera_Movil c INNER JOIN Espacio e ON c.Id_espacio=e.Id_Espacio WHERE Id_cochera_Movil = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                CocheraMovil cocheraMovil = new CocheraMovil(id);
                LlenarObjeto(cocheraMovil, dset.Tables[0].Rows[0]);
                return cocheraMovil;
            }
            else
            {
                return null;
            }

        }

     


        public static List<CocheraMovil> Listar()
        {
            string query = "SELECT * FROM Cochera_Movil c INNER JOIN Espacio e ON c.Id_espacio=e.Id_Espacio";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<CocheraMovil> listaCocheraMovils = new List<CocheraMovil>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    CocheraMovil cocheraMovil = new CocheraMovil(int.Parse(dr["id_Cochera_Movil"].ToString()));
                    LlenarObjeto(cocheraMovil, dr);
                    listaCocheraMovils.Add(cocheraMovil);
                }

            }
            return listaCocheraMovils;
        }

        public static List<CocheraMovil> ListarPorPlaya(int id)
        {
            string query = "SELECT * FROM Cochera_Movil c INNER JOIN Espacio e ON c.Id_espacio=e.Id_Espacio WHERE e.Id_playa = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<CocheraMovil> listaCocheraMovils = new List<CocheraMovil>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    CocheraMovil cocheraMovil = new CocheraMovil(int.Parse(dr["id_Cochera_Movil"].ToString()));
                    LlenarObjeto(cocheraMovil, dr);
                    listaCocheraMovils.Add(cocheraMovil);
                }

            }
            return listaCocheraMovils;
        }

        internal static void LlenarObjeto(CocheraMovil cocheraMovil, DataRow dr)
        {
            cocheraMovil.HoraSalida = TimeSpan.Parse(dr["hora_salida"].ToString());
            cocheraMovil.HoraEntrada = TimeSpan.Parse(dr["Hora_entrada"].ToString());
            cocheraMovil.IdEspacio = int.Parse(dr["id_espacio"].ToString());
            EspacioDAL.LlenarObjeto(cocheraMovil, dr);
        }
    }
}
