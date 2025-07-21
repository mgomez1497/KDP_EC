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
    public class VillagesRepository : IVillages
    {
        private readonly SqlDbManager _db;

        public VillagesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Villages> GetVillagesbyId(Guid id)
        {
            var sql = "EXEC [dbo].[spGetVillagesbyId] @VillageId";

            var parameters = new Dictionary<string, object>
            {
                ["@VillageId"] = id
            };

            var table = _db.ExecuteQuery(sql, parameters);
            var villages = new List<Villages>();

            foreach(DataRow row in table.Rows)
            {
                var village = new Villages
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Status = Conv.ABool(row["Status"].ToString()),
                    CityId = Conv.AGuid(row["CityId"].ToString()),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),

                };

                villages.Add(village);
            }
            return villages;
        }

        public List<Villages> GetVillages()
        {
            var sql = "EXEC [dbo].[spGetVillages]";
            var table = _db.ExecuteQuery(sql, null);
            var villages = new List<Villages>();
            
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    var village = new Villages
                    {
                        Id = Conv.AGuid(row["Id"]),
                        Name = Conv.AStr(row ["Name"]),
                        Status = Conv.ABool(row["Status"]),
                        CityId = Conv.AGuid(row["CityId"]),
                        CreatedAt = Conv.AFecha(row["CreatedAt"]),
                        UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                        DeletedAt = Conv.AFecha(row["DeletedAt"])
                    };
                    villages.Add(village);
                }

                catch(Exception e)
                {
                    var sing = e.Message;
                }
            }
            return villages;
        }

     


    }
}
