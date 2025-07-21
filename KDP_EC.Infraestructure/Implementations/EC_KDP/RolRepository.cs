using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class RolRepository : IRol
    {
        private readonly SqlDbManager _db;

        public RolRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Rols> GetRols()
        {
            var sql = " EXEC [dbo].[spGetRol]";
            var result = _db.ExecuteQuery(sql, null);
            var rols = new List<Rols>();

            foreach (DataRow row in result.Rows)
            {
                var rol = new Rols
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                };
                rols.Add(rol);
            }

            return rols;
        }
    }
}
