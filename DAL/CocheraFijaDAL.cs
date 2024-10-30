using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CocheraFijaDAL
    {
        static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("Cochera_Fija");
            id += 1;
            return id;
        }

        public static int Guardar(CocheraFija cocheraFija)
        {
            if (cocheraFija.IdCocheraFija == 0)
            {
                cocheraFija.IdCocheraFija = ProximoId();
                string query = "INSERT INTO Cochera_Fija (Id_Cochera_Fija, Valor_Mes, Id_Espacio) VALUES (" + cocheraFija.IdCocheraFija + ", " + cocheraFija.ValorMes + ","+cocheraFija.IdEspacio+")";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE Cochera_Fija SET Valor_Mes = " + cocheraFija.ValorMes + " WHERE Id_Cochera_Fija = " + cocheraFija.IdCocheraFija;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE Cochera_Fija WHERE Id_Cochera_Fija = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }

        public static CocheraFija Obtener(int id)
        {
            string query = "SELECT * FROM Cochera_Fija c INNER JOIN Espacio e ON c.Id_espacio=e.Id_Espacio WHERE Id_Cochera_Fija = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                CocheraFija cocheraFija = new CocheraFija(id);
                LlenarObjeto(cocheraFija, dset.Tables[0].Rows[0]);
                return cocheraFija;
            }
            else
            {
                return null;
            }

        }

        public static List<CocheraFija> Listar()
        {
            string query = "SELECT * FROM Cochera_Fija c INNER JOIN Espacio e ON c.Id_espacio=e.Id_Espacio";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<CocheraFija> listaCocheraFijas = new List<CocheraFija>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    CocheraFija cocheraFija = new CocheraFija(int.Parse(dr["Id_Cochera_Fija"].ToString()));
                    LlenarObjeto(cocheraFija, dr);
                    listaCocheraFijas.Add(cocheraFija);
                }

            }
            return listaCocheraFijas;
        }

        internal static void LlenarObjeto(CocheraFija cocheraFija, DataRow dr)
        {
            cocheraFija.ValorMes = float.Parse(dr["Valor_Mes"].ToString());
            cocheraFija.IdEspacio = int.Parse(dr["id_espacio"].ToString());
            EspacioDAL.LlenarObjeto(cocheraFija, dr);

        }

    }
}
