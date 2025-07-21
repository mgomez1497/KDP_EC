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
    public class CompaniesRepository : ICompany
    {

        private readonly SqlDbManager _db;

        public CompaniesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Company> GetCompanies()
        {
            var sql = "EXEC [dbo].[spGetCompanies]";
           var table = _db.ExecuteQuery(sql, null);
            var companies = new List<Company>();

            foreach (DataRow row in table.Rows)
            {
                var company = new Company
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"])
                };
                companies.Add(company);
            }
            return companies;
        }

        public List<Company> GetCompaniesbyId(Guid Id)
        {
            var sql = "EXEC [dbo].[spGetCompaniesbyId] @Id ";

            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = Id
            };

            var table = _db.ExecuteQuery(sql, parameters);
            var companies = new List<Company>();

            foreach (DataRow row in table.Rows)
            {
                var company = new Company
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"])
                };

                companies.Add(company);
            }
            return companies;
        }
    }
}
