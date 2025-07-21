using KDP_EC.Core.Interfaces;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HR.Infraestructure.DBContexto.Conversiones;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class Renewal_TypesRepository : IRenewal_Types
    {
        private readonly SqlDbManager _db;

        public Renewal_TypesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Renewal_Types> GetRenewalTypes()
        {
            var sql = "EXEC [dbo].[spGetRenewal]";
            var table = _db.ExecuteQuery(sql, null);
            var renewalTypes = new List<Renewal_Types>();

            foreach (DataRow row in table.Rows)
            {
                var renewalType = new Renewal_Types
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    Status = Conv.ABool(row["Status"].ToString())
                };
                renewalTypes.Add(renewalType);
            }
            return renewalTypes;
        }
    }
}
