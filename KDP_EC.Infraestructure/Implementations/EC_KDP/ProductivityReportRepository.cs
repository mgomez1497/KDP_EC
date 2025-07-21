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
    public class ProductivityReportRepository: IProductivityReport
    {
        private readonly SqlDbManager _db;
        public ProductivityReportRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<ProductivityReport> GetProductivityReportByFarm_MultiYear(Guid farmId)
        {
           var sql = "[dbo].[spGetProductivityReportByFarm_MultiYear] @FarmId";
           var parameters = new Dictionary<string, object>
           {
               ["@FarmId"] = farmId
           };
            var table = _db.ExecuteQuery(sql, parameters);
            var productivityReports = new List<ProductivityReport>();

            foreach (DataRow row in table.Rows)
            {
                var productivityReport = new ProductivityReport
                {
                    Year = Conv.AEntero(row["Year"]),
                    Orders = Conv.AEntero(row["Orders"]),
                    Name = row["Name"].ToString(),
                    Value = Conv.ADec(row["Value"]),
                    Ideal = Conv.ADec(row["Ideal"]),
                    FarmId = Conv.AGuid(row["FarmId"])
                };
                productivityReports.Add(productivityReport);
            }
            return productivityReports;
        }


    }
}
