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
    public class CitiesRepository : ICities
    {
        private readonly SqlDbManager _db;

        public CitiesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Cities>GetCities()
        {
            var sql = "EXEC [dbo].[spGetCities]";
            var table = _db.ExecuteQuery(sql, null);
            var cities = new List<Cities>();

            foreach (DataRow row in table.Rows)
            {
                var city = new Cities
                {
                    Id = Conv.AGuid(row["Id"]),
                    Name = row["Name"].ToString(),
                    StateId = Conv.AGuid(row["StateId"]),
                    Status = Conv.ABool(row["Status"]),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                };
                cities.Add(city);
            }
            return cities;
        }
    }
}
