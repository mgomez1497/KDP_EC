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
    public class ActivitiesRepository: IActivities
    {
        private readonly SqlDbManager _db;

        public ActivitiesRepository(SqlDbManager db)
        {
            _db = db;
        }
        public List<Activities> GetActivities()
        {
            var sql = "EXEC [dbo].[spGetActivities]";
            var table = _db.ExecuteQuery(sql, null);
            var activities = new List<Activities>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    var activity = new Activities
                    {
                        Id = Conv.AGuid(row["Id"]),
                        ActivityName = row["ActivityName"].ToString(),
                        Description = row["Description"].ToString(),
                        ActivityTypeId = Conv.AGuid(row["ActivityTypeId"]),
                        CostCenterId = Conv.AGuid(row["CostCenterId"]),
                        CreatedAt = Conv.AFecha(row["CreatedAt"]),
                        UpdatedAt = Conv.AFecha(row["UpdatedAt"])
                    };
                    activities.Add(activity);
                }
               
            }
            return activities;

        }


    }
}
