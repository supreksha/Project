using UserData.Models;
using System;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Configuration;

namespace UserData.DAL
{
    public class UserRepository : IUserRepository
    {
        //read connection string from config key
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        public UserEntity GetUserById(uint id)
        {
            UserEntity user = new UserEntity();
            try
            {
                var param = new DynamicParameters();
                param.Add("v_Id", id);
                using (var connection = new MySqlConnection(_conString))
                {
                    user = connection.Query<UserEntity>("GetUsers", param, commandType: CommandType.StoredProcedure).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        /// <summary>
        /// Update, Insert, Delete user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns the respective id if successfull else -1 </returns>
        public int UpdateUser(UserEntity user)
        {
            int userId = -1;
            try
            {
                var param = new DynamicParameters();
                param.Add("v_Id", user.Id);
                param.Add("v_FirstName", string.IsNullOrWhiteSpace(user.FirstName) ? Convert.DBNull : user.FirstName, DbType.String);
                param.Add("v_LastName", string.IsNullOrWhiteSpace(user.LastName) ? Convert.DBNull : user.LastName, DbType.String);
                param.Add("v_EmailId", string.IsNullOrWhiteSpace(user.EmailId) ? Convert.DBNull : user.EmailId, DbType.String);
                param.Add("v_MobileNumber", string.IsNullOrWhiteSpace(user.MobileNumber) ? Convert.DBNull : user.MobileNumber, DbType.String);
                param.Add("v_IsActive", user.IsActive);
                using (var connection = new MySqlConnection(_conString))
                {
                    userId = connection.Query("UpdateUser", param, commandType: CommandType.StoredProcedure).First().UserId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userId;
        }
    }
}