using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class PropertyProccessor
    {
        public static List<PropertyModel> LoadProperty()
        {
            string sql = "spGetPropertyGrid";
            return SqlDataAccess.LoadData<PropertyModel>(sql);
        }
        public static List<PropertyModel> LoadSharedProperty()
        {
            string sql = "spGetSharedProperty";
            return SqlDataAccess.LoadData<PropertyModel>(sql);
        }

        public static List<Image> LoadImageById(int PropertyId)
        {
            string sql = "spGetImagesById";
            return SqlDataAccess.LoadData<Image>(sql, PropertyId);
        }

        public static List<int> SharedProfile<T>(T parameters)
        {
            string sql = "spGetSharedReservation";
            return SqlDataAccess.LoadGenericList(sql,parameters);
        }

        //public static List<SignUpModel> GetTenant(int PropertyId)
        //{
        //    string sql = "spGetTenantById";
        //    return SqlDataAccess.LoadData<SignUpModel>(sql, PropertyId);
        //}

        public static void InsertShared(int PropertyId, string Shared)
        {
            string sql = $"insert into Shared values ({PropertyId}, '{Shared}')";
            SqlDataAccess.SaveData(sql);
        }

        public static void UpdateConfirmation<T>(T parameters)
        {
            string sql = "spUpdateConfirmation";
            SqlDataAccess.Save(sql,parameters);
        }

        public static void SendMessage<T>(T parameters)
        {
            string sql = "spInsertMessage";
            SqlDataAccess.Save(sql, parameters);
        }

        public static List<MessageModel> GetTenantMessages(int id)
        {
            string sql = "spGetTenantMessage";
            return SqlDataAccess.LoadData<MessageModel>(sql, id);
        }

        public static List<MessageModel> GetLandlordMessages(int id)
        {
            string sql = "spGetLandlordMessage";
            return SqlDataAccess.LoadData<MessageModel>(sql, id);
        }

        public static List<PropertyModel> LoadPropertyById(int PropertyId)
        {
            string sql = "spGetPropertyById";
            return SqlDataAccess.LoadData<PropertyModel>(sql,PropertyId);
        }

        public static List<ReservationModel> GetReservations(int PropertyId)
        {
            string sql = "spGetReservationById";
            return SqlDataAccess.LoadData<ReservationModel>(sql, PropertyId);
        }

        public static List<ReservationModel> GetLandlordReservations(int PropertyId)
        {
            string sql = "spGetLandlordReservation";
            return SqlDataAccess.LoadData<ReservationModel>(sql, PropertyId);
        }

        public static List<PropertyModel> LoadPropertyByLandlordId(int LandlordId)
        {
            string sql = "spGetPropertyByLandlordId";
            return SqlDataAccess.LoadData<PropertyModel>(sql, LandlordId);
        }

        public static string GetLandlordName(int id)
        {
            string sql = "spGetLandlordName";
            return SqlDataAccess.GetName(sql, id);
        }

        public static string DeleteReservation(int id)
        {
            string sql = "spDeleteReservation";
            return SqlDataAccess.GetName(sql, id);
        }

        public static List<PropertyModel> GetProperty<T>(T parameters)
        {
            string sql = "spGetPropertySearch";
            
            List<PropertyModel> rows = SqlDataAccess.LoadDataBySearch(sql, parameters);
            return rows;
        }

        public static List<PropertyModel> GetPropertyByLoc<T>(T parameters)
        {
            string sql = "spGetPropertySearchByLoc";

            List<PropertyModel> rows = SqlDataAccess.LoadDataBySearch(sql, parameters);
            return rows;
        }

        public static List<PropertyModel> GetPropertyByType<T>(T parameters)
        {
            string sql = "spGetPropertySearchByType";

            List<PropertyModel> rows = SqlDataAccess.LoadDataBySearch(sql, parameters);
            return rows;
        }

        public static List<PropertyModel> LoadPropertyBySearch(string Location, string Room, string Person)
        {
            string sql = "spGetPropertyBySearch";
            var d = new
            {
                Location,Room,Person
            };
            List<PropertyModel> rows = SqlDataAccess.LoadDataBySearch(sql,d);
            return rows;
        }

        public static int SaveProperty<T>(T parameters)
        {
            string sql = "dbo.spStoreProperty";
            return SqlDataAccess.SaveData(sql, parameters);
        }

        public static void SaveReservation<T>(T parameters)
        {
            string sql = "dbo.spInsertReservation";
            SqlDataAccess.Save(sql, parameters);
        }

        public static int GetLandlordId(string Username)
        {
            string sql = "dbo.spGetLandlordId";
            //string sql = $"SELECT SignupId from SignupUserRoles where Username = '{Username}'";
            return SqlDataAccess.Load(sql,Username);
        }

        public static int GetLandlordByPropertyId(int Id)
        {
            string sql = "dbo.spGetLandlordByPropertyId";
            return SqlDataAccess.LoadId(sql, Id);
        }

        public static void InsertProperty<T>(T parameters)
        {
            string sql = "dbo.spInsertProperty";
            SqlDataAccess.Save(sql, parameters);
        }

        public static void ImageUpload(int id, List<string> filePath)
        {
            foreach (var item in filePath)
            {
                string sql = $"Insert into PropertyImages values ({id}, '{item}')";
                SqlDataAccess.SaveData(sql);
            }

        }

        public static void UserImageUpload(int id, List<string> filePath)
        {
            foreach (var item in filePath)
            {
                string sql = $"Insert into UserImage values ({id}, '{item}')";
                SqlDataAccess.SaveData(sql);
            }

        }

        public static List<PropertyModel> GetImage(int id)
        {
            string sql = $"Select ImagePath From PropertyImages where PropertyId = {id} ";
            List<PropertyModel> images = SqlDataAccess.LoadImage(sql);
            return images;
        }

        public static void DeleteProperty(int Id)
        {
            string sql = "spDeleteProperty";
            SqlDataAccess.Save(sql, Id);
        }
        
        public static void SendContactMessage<T>(T parameters)
        {
            string sql = "spInsertContactMessage";
            SqlDataAccess.Save(sql, parameters);
        }
    }
}
