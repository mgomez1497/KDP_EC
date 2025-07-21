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
    public class StageOfCultRepository:IStageOfCult
    {
        private readonly SqlDbManager _db;

        public StageOfCultRepository(SqlDbManager db)
        {
            _db = db;
        }
        public List<StageOfCult> GetStageOfCults()
        {
            var sql = "EXEC [dbo].[spGetStagesOfCult]";
            var table = _db.ExecuteQuery(sql, null);
            var stagesOfCult = new List<StageOfCult>();
            foreach (DataRow row in table.Rows)
            {
                var stage = new StageOfCult
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"].ToString()),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"].ToString()),
                    DeletedAt = Conv.AFecha (row["DeletedAt"].ToString()),
                    Status = Conv.ABool(row["Status"].ToString())
                };
                stagesOfCult.Add(stage);
            }
            return stagesOfCult;
        }
    }
}
