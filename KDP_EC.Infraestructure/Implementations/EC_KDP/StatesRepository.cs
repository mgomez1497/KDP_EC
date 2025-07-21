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
    public class StatesRepository : IStates
    {
        private readonly SqlDbManager _db;

        public StatesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<States> GetStates()
        {
            var sql = "EXEC [dbo].[spGetStates]";
            var table = _db.ExecuteQuery(sql, null);
            var states = new List<States>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    var state = new Core.Models.States
                    {
                        Id = Conv.AGuid(row["Id"]),
                        Name = row["Name"].ToString(),
                        Status= Conv.ABool(row["Status"]),
                        CountryId = Conv.AGuid(row["CountryId"]),
                        CreatedAt = Conv.AFecha(row["CreatedAt"]),
                        UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                        DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    };
                    states.Add(state);
                }
            }
            return states;
        }
    }
}
