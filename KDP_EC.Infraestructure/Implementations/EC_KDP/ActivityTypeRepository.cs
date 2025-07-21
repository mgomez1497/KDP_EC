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
    public class ActivityTypeRepository : IActivityType
    {
        private readonly SqlDbManager _db;

        public ActivityTypeRepository(SqlDbManager db)
        {
            _db = db;
        }

       

        public List<ActivityType> GetActivityTypes()
        {
            var sql = "EXEC [dbo].[spGetActivityType] ";
            var table = _db.ExecuteQuery(sql, null);
            var activityTypes = new List<ActivityType>();

            foreach (DataRow row in table.Rows)
            {
                var activityType = new ActivityType
                {
                    Id = Conv.AGuid(row["Id"]),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    Status = Conv.ABool(row["Status"])

                };
                activityTypes.Add(activityType);
            }

            return activityTypes;
        }

        
    }
}
