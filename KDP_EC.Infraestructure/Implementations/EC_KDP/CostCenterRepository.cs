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
    public class CostCenterRepository : ICostCenter
    {
        private readonly SqlDbManager _db;

        public CostCenterRepository(SqlDbManager db)
        {
            _db = db;
        }


        public List<CostCenter> GetCostCenters()
        {
            var sql = "EXEC [dbo].[spGetCostCenters]";
            var table = _db.ExecuteQuery(sql,null);
            var costCenters = new List<CostCenter>();
            foreach (DataRow row in table.Rows)
            {
                var costCenter = new CostCenter
                {
                    Id = Conv.AGuid(row["Id"]),
                    Name = row["Name"].ToString(),
                    Type = row["Type"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    Status = Conv.ABool(row["Status"]),
                    StageOfCultId = Conv.AGuid(row["StageOfCultId"])
                };
                costCenters.Add(costCenter);
            }
            return costCenters;
        }
    }
}
