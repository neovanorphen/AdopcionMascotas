using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Adopcion.Utils
{
    public  static class BaseData
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["AdopcionDB"].ConnectionString;
        public static void ExecuteCommand(MySqlCommand command)
        {
            
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public static DataTable GetDataTable(MySqlCommand command)
        {
            var dt = new DataTable();
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                command.Connection = conn;
                var sqlda = new MySqlDataAdapter(command);
                sqlda.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public static DataSet GetDataSet(MySqlCommand command)
        {
            var ds = new DataSet();
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                command.Connection = conn;
                var sqlda = new MySqlDataAdapter(command);
                sqlda.Fill(ds);
                conn.Close();
            }
            return ds;

        }
    }
}