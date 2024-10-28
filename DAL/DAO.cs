using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Configuration;

namespace DAL
{
    public class DAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

        public DataSet ExecuteDataSet(string query)
        {
            try
            {
                SqlDataAdapter dTable = new SqlDataAdapter(query, conn);
                DataSet dataSet = new DataSet();

                dTable.Fill(dataSet);

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public int ObtenerUltimoId(string tabla)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(Id_"+tabla+"),0) FROM " + tabla, conn);
                conn.Open();
                return int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
    }
}
