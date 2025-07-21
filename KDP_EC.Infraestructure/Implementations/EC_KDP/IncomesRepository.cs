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
    public class IncomesRepository : IIncomes
    {
        private readonly SqlDbManager _db;

        public IncomesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public int CreateIncomes(Incomes incomes)
        {
            var sql = @"EXEC [dbo].[spCreateIncomes]
                @Id, 
                @TypeId,
                @Date,      
                @TotalValue,
                @Kgsold,
                @HelthyAlmondPercent,
                @PoorCoffeePercent,
                @DecreasePercent,
                @PerformanceFactor,
                @FarmId,
                @CreatedAt,
                @UpdatedAt,
                @DeletedAt,
                @PercentageKg,
                @Invoice,
                @Realizado OUTPUT,       
                @Error OUTPUT";

            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = incomes.Id,
                ["@TypeId"] = incomes.TypeId,
                ["@Date"] = incomes.Date,
                ["@TotalValue"] = incomes.TotalValue,
                ["@Kgsold"] = incomes.KgSold,
                ["@HelthyAlmondPercent"] = incomes.HealthyAlmondPercent,
                ["@PoorCoffeePercent"] = incomes.PoorCoffeePercent,
                ["@DecreasePercent"] = incomes.DecreasePercent,
                ["@PerformanceFactor"] = incomes.PerformanceFactor,
                ["@FarmId"] = incomes.FarmId,
                ["@CreatedAt"] = incomes.CreatedAt,
                ["@UpdatedAt"] = incomes.UpdatedAt,
                ["@DeletedAt"] = incomes.DeletedAt,
                ["@PercentageKg"] = incomes.PercentageKg,
                ["@Invoice"] = incomes.Invoice,
                ["@Realizado"] = 0,
                ["@Error"] = ""
            };
            var output = _db.ExecuteStoredProcedureWithOutput(sql, parameters);
            return Convert.ToInt32(output["@Realizado"]);
        }

        public List<Incomes> GetIncomes()
        {
            var sql = "EXEC [dbo].[spGetIncomes]";
            var table = _db.ExecuteQuery(sql, null);
            var incomesList = new List<Incomes>();
            foreach (DataRow row in table.Rows)
            {
                var income = new Incomes
                {
                    Id = Conv.AGuid(row["Id"]),
                    TypeId = Conv.AGuid(row["TypeId"]),
                    Date = Conv.AFecha(row["Date"]),
                    TotalValue = Conv.ADec(row["TotalValue"]),
                    KgSold = Conv.ADec(row["KgSold"]),
                    HealthyAlmondPercent = Conv.ADec(row["HealthyAlmondPercent"]),
                    PoorCoffeePercent = Conv.ADec(row["PoorCoffeePercent"]),
                    DecreasePercent = Conv.ADec(row["DecreasePercent"]),
                    PerformanceFactor = Conv.ADec(row["PerformanceFactor"]),
                    FarmId = Conv.AGuid(row["FarmId"]),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    PercentageKg = Conv.ADec(row["PercentageKg"]),
                    Invoice= row["Invoice"].ToString()
                };
                incomesList.Add(income);
            }
            return incomesList;
        }

        public List<Incomes> GetIncomesByFarmId(Guid farmId)
        {
            var sql = "EXEC [dbo].[spGetIncomesByFarmId] @FarmId";
            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = farmId
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var incomesList = new List<Incomes>();
            foreach (DataRow row in table.Rows)
            {
                var income = new Incomes
                {
                    Id = Conv.AGuid(row["Id"]),
                    TypeId = Conv.AGuid(row["TypeId"]),
                    Date = Conv.AFecha(row["Date"]),
                    TotalValue = Conv.ADec(row["TotalValue"]),
                    KgSold = Conv.ADec(row["KgSold"]),
                    HealthyAlmondPercent = Conv.ADec(row["HealthyAlmondPercent"]),
                    PoorCoffeePercent = Conv.ADec(row["PoorCoffeePercent"]),
                    DecreasePercent = Conv.ADec(row["DecreasePercent"]),
                    PerformanceFactor = Conv.ADec(row["PerformanceFactor"]),
                    FarmId = Conv.AGuid(row["FarmId"]),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    PercentageKg = Conv.ADec(row["PercentageKg"])
                };
                incomesList.Add(income);
            }
            return incomesList;
        }
    }
}
