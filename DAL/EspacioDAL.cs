using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EspacioDAL
    {
        static int id;
        private static int ProximoId()
        {
            if (id == 0)
                id = (new DAO()).ObtenerUltimoId("Espacio");
            id += 1;
            return id;
        }

        public static int Guardar(Espacio espacio)
        {
            if (espacio.IdEspacio== 0)
            {
                espacio.IdEspacio = ProximoId();
                string query = "INSERT INTO Espacio (Id_Espacio,Piso, Porcentaje_Valor, Tamano, id_vehiculo, id_playa) VALUES (" + espacio.IdEspacio + ", " + espacio.Piso + ", " + espacio.PorcentajeValor + ",'" + espacio.Tamano + "' , "+1+","+ espacio.IdPlaya+")";
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }
            else
            {
                string query = "UPDATE Espacio SET Piso = " + espacio.Piso + ", Porcentaje_Valor = " + espacio.PorcentajeValor + ", Tamano ='" + espacio.Tamano + "', id_vehiculo = " + 1+ " WHERE Id_Espacio = " + espacio.IdEspacio;
                DAO dao = new DAO();
                return dao.ExecuteNonQuery(query);
            }

        }

        public static int Eliminar(int id)
        {
            string query = "DELETE Espacio WHERE Id_Espacio = " + id;
            DAO dao = new DAO();
            return dao.ExecuteNonQuery(query);
        }

        public static Espacio Obtener(int id)
        {
            string query = "SELECT * FROM Espacio WHERE Id_Espacio = " + id;

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                Espacio espacio = new Espacio(id);
                LlenarObjeto(espacio, dset.Tables[0].Rows[0]);
                return espacio;
            }
            else
            {
                return null;
            }

        }

        public static List<Espacio> Listar()
        {
            string query = "SELECT * FROM Espacio";

            DAO dao = new DAO();

            DataSet dset = dao.ExecuteDataSet(query);
            List<Espacio> listaEspacios = new List<Espacio>();

            if (dset.Tables.Count > 0 && dset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dset.Tables[0].Rows)
                {
                    Espacio espacio = new Espacio(int.Parse(dr["id_Espacio"].ToString()));
                    LlenarObjeto(espacio, dr);
                    listaEspacios.Add(espacio);
                }

            }
            return listaEspacios;
        }

        internal static void LlenarObjeto(Espacio espacio, DataRow dr)
        {
            espacio.Tamano = dr["Tamano"].ToString();
            espacio.PorcentajeValor = int.Parse(dr["Porcentaje_Valor"].ToString());
            espacio.IdEspacio = int.Parse(dr["id_espacio"].ToString());
            espacio.Piso = int.Parse(dr["Piso"].ToString());
            Vehiculo v  = new Vehiculo();
            v.IdVehiculo = 1;
            espacio.Vehiculo = v;
        }
    }
}
