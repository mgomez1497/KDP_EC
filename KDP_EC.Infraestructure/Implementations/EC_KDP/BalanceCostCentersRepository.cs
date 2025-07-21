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
    public class BalanceCostCentersRepository : IBalanceCostCenters
    {
        private readonly SqlDbManager _db;

        public BalanceCostCentersRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<BalanceCostCenters> GetBalanceCostCentersByFarm(Guid farmId)
        {
            var sql = "[dbo].[sp_BalanceCostCentersByFarm] @FarmId";
            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = farmId
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var balanceCostCenters = new List<BalanceCostCenters>();

            foreach (DataRow row in table.Rows)
            {
                var balanceCostCenter = new BalanceCostCenters
                {
                    
                    CostCenterId = Conv.AGuid(row["CostCenterId"]),
                    CostCenterName = row["CostCenterName"].ToString(),
                    FarmAverage = Conv.ADec(row["FarmAverage"]),
                    ExportAverage = Conv.ADec(row["ExportAverage"])

                };
                balanceCostCenters.Add(balanceCostCenter);

            }
            return balanceCostCenters;
        }
    }
}
