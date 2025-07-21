using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class URCRepositoritory : IURC
    {
        private readonly SqlDbManager _db;

        public URCRepositoritory(SqlDbManager db)
        {
            _db = db;
        }

        public bool CreateURC(URC urc)
        {
            var sql = "EXEC [dbo].[spCreateUserRolCompany] @UserId, @RolId, @CompanyId";
            var parameters = new Dictionary<string, object>
            {
                ["@UserId"] = urc.Id_User,
                ["@RolId"] = urc.Id_Rol,
                ["@CompanyId"] = urc.Id_Company
            };
            var result = _db.ExecuteNonQuery(sql, parameters);
            return result > 0;
        }
    }
}
