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
    public class CoffeeSalesRepository:ICoffeeSalesRep
    {
        private readonly SqlDbManager _db;
        public CoffeeSalesRepository(SqlDbManager db)
        {
            _db = db;
        }
        public List<CoffeeSalesRep> GetCoffeeSalesReps(string FarmId)
        {
            var sql = "[dbo].[spGetCoffeeSalesReport] @FarmId";
            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = FarmId
            };

            var table = _db.ExecuteQuery(sql, parameters);
            var coffeeSalesReps = new List<CoffeeSalesRep>();

            foreach (DataRow row in table.Rows)
            {
                var coffeeSalesRep = new CoffeeSalesRep
                {
                    Year = Conv.AEntero(row["Year"]),
                    Orders = Conv.AEntero(row["Orders"]),
                    Name = row["Name"]?.ToString(),
                    KgCereza = Conv.ADec(row["kgCereza"]),
                    KgPergamino = Conv.ADec(row["kgPergamino"]),
                    Percents = Conv.ADec(row["percents"]),
                    ConvertFactor = Conv.ADec(row["convertFactor"]),
                    FarmId = Conv.AGuid(row["FarmId"])
                };
                coffeeSalesReps.Add(coffeeSalesRep);
            }

            return coffeeSalesReps;
        }
    }
}
