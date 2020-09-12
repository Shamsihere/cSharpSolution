using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class SignupProcessor
    {
        //public static List<SignupProcessor> LoadData()
        //{
        //    string sql = "spGetProperty";
        //    return SqlDataAccess.LoadData<PropertyModel>(sql);
        //}

        public static List<SignUpModel> Property<T>(T parameters)
        {
            string sql = "dbo.spStoreProperty";
            return SqlDataAccess.Signup(sql, parameters).ToList<SignUpModel>();

        }


        public static List<SignUpModel> SignupUser<T>(T parameters)
        {
            string sql = "dbo.spSignup";
            return SqlDataAccess.Signup(sql, parameters).ToList<SignUpModel>();

        }

        public static int LoadLandlordById(int UserId)
        {
            string sql = "spGetUserId";
            return SqlDataAccess.Load(sql, UserId);
        }

        public static List<SignUpModel> LoadUserById(int id)
        {
            string sql = "spGetTenantById";
            return SqlDataAccess.LoadData<SignUpModel>(sql, id);
        }

        public static List<SignUpModel> LoadUserByUsername(string username)
        {
            string sql = "spGetUserProfile";
            return SqlDataAccess.LoadData(sql, username);
        }

        public static List<SignUpModel> TenantSignup<T>(T parameters)
        {
            string sql = "dbo.spStoreTenant";
            return SqlDataAccess.Signup(sql, parameters).ToList<SignUpModel>();

        }

        public static List<SignUpModel> LandlordSignup<T>(T parameters)
        {
            string sql = "dbo.spStoreLandlord";
            return SqlDataAccess.Signup(sql, parameters).ToList<SignUpModel>();

        }

        public static void SignupUserPassword<T>(T parameters)
        {
            string sql = "dbo.spSignupUserPasswordRole";
            SqlDataAccess.Save(sql, parameters);
        }

        public static List<SignUpModel> GetTenant(int PropertyId)
        {
            string sql = "spGetTenantById";
            return SqlDataAccess.LoadData<SignUpModel>(sql, PropertyId);
        }

        public static List<SignUpModel> GetTenants()
        {
            string sql = "spGetTenants";
            return SqlDataAccess.LoadData<SignUpModel>(sql);
        }

        public static List<SignUpModel> GetLandlords()
        {
            string sql = "spGetLandlords";
            return SqlDataAccess.LoadData<SignUpModel>(sql);
        }

        public static void DeleteUser(int id)
        {
            string sql = "spDeleteUser";
            SqlDataAccess.Save(sql, id);
        }

    }
}
