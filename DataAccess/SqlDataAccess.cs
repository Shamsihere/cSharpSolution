using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "RentLentDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using(IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {

                return cnn.Query<T>(sql, commandType: CommandType.Text).ToList();
            }
        }

        public static List<SignUpModel> LoadData(string sql, string username)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<SignUpModel>(sql, new { username }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static int Load<T>(string sql, T username)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {

                return Convert.ToInt32(cnn.ExecuteScalar(sql, new { username }, commandType: CommandType.StoredProcedure));
            }
        }

        public static int LoadId(string sql, int id)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {

                return Convert.ToInt32(cnn.ExecuteScalar(sql, new { id }, commandType: CommandType.StoredProcedure));
            }
        }

        public static List<PropertyModel> LoadImage(string sql)
        {
            using(IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<PropertyModel>(sql, commandType: CommandType.Text).ToList();
            }
        }

        public static List<T> LoadData<T>(string storedProcedure, int Id)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                List<T> rows = cnn.Query<T>(storedProcedure, new { Id } , commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public static List<int> LoadGenericList<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                List<int> rows = cnn.Query<int>(storedProcedure, parameters , commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public static List<PropertyModel> LoadDataBySearch<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                List<PropertyModel> rows = cnn.Query<PropertyModel>(storedProcedure, parameters , commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public static void SaveData(string query)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                cnn.Query(query, commandType: CommandType.Text);

            }
        }



        public static void Save<T>(string StoredProcedure, T parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                cnn.ExecuteScalar(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public static int SaveData<T>(string StoredProcedure, T parameters)
        {
            using(IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var data = cnn.ExecuteScalar(StoredProcedure,  parameters , commandType: CommandType.StoredProcedure);
                return Convert.ToInt32(data);
            }
        }

        public static List<SignUpModel> Signup<T>(string query, T parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<SignUpModel>(query, parameters, commandType: CommandType.StoredProcedure).ToList<SignUpModel>();
                
            }
        }
        public static string GetName(string query, int Id)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.QueryFirstOrDefault<string>(query, new { Id }, commandType: CommandType.StoredProcedure);

            }
        }


    }
}
