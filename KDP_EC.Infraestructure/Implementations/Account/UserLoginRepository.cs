using KDP_EC.Core.Interfaces.Account;
using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.Account
{
    public class UserLoginRepository : IUsersLogin
    {
        private readonly SqlDbManager _db;

        public UserLoginRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<UserInfoViewModel> GetUsersInfo(Guid idUser)
        {
            var sql = "EXEC [dbo].[spGetUserInfo] @IdUser";
            var parameters = new Dictionary<string, object>
            {
                ["@IdUser"] = idUser
            };

            var userList = new List<UserInfoViewModel>();
            var table = _db.ExecuteQuery(sql, parameters);

            foreach (DataRow row in table.Rows)
            {
                var user = new UserInfoViewModel
                {
                    Identification = row["Identification"].ToString(),
                    NombreCompleto= row["NombreCompleto"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Email = row["Email"].ToString(),
                    Id_Rol = Guid.Parse(row["Id_Rol"].ToString()),
                    Id_Company = Guid.Parse(row["Id_Company"].ToString()),
                    Id = Guid.Parse(row["Id"].ToString())
                };

                userList.Add(user);
            }

            return userList;
        }



        public bool Login(string username, string password, out Guid? userId)
        {
            var sql = "EXEC [dbo].[sp_UserLogin] @Username, @Password";
            var parameters = new Dictionary<string, object>
            {
                ["@Username"] = username,
                ["@Password"] = password
            };

            var result = _db.ExecuteScalar(sql, parameters);

            if (result != null)
            {
                userId = (Guid)result; 
                return true;
            }

            userId = null;
            return false;
        }

        public bool CreateUser(Guid Id, string username, string password)
        {
            var sql = "EXEC [dbo].[spCreateUser] @Id, @Username, @Password,@Status";
            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = Id,
                ["@Username"] = username,
                ["@Password"] = password,
                ["@Status"] = true
            };
            var result = _db.ExecuteNonQuery(sql, parameters);
            return result > 0;
        }


    }
}
