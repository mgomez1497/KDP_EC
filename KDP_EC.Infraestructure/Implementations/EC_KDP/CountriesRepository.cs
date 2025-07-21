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
    public class CountriesRepository : ICountries
    {
        private readonly SqlDbManager _db;

        public CountriesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List <Countries> GetCountries()
        {
            var sql = "EXEC [dbo].[GetCountries]";
            var table = _db.ExecuteQuery(sql, null);
            var Countries = new List<Countries>();

            foreach (DataRow row in table.Rows)
            {
                var country = new Countries
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    Status = bool.Parse(row["Status"].ToString()),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                };
                Countries.Add(country);
            }
            return Countries;
        }
    }
}
