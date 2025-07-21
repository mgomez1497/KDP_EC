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
    public class ExpensesRepository:IExpenses
    {
        private readonly SqlDbManager _db;

        public ExpensesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public int CretateExpense(Expenses expenses)
        {
            var sql = @"Exec [dbo].[spCreateExpenses]
	                    @Id,
	                    @FarmId,
	                    @StageOfCultivationId,
	                    @Date,
	                    @WagesNumber,
	                    @AmmountSuplies,
	                    @AmmountKgCollected,
	                    @CreatedAt,
                        @UpdatedAt,
                        @DeletedAt,
	                    @TotalValue,
	                    @Description,
	                    @FamiliarWages,
                        @ActivityId,
                        @CostCenterId,
	                    @Realizado OUTPUT,       
                        @Error OUTPUT ";
            var parameters = new Dictionary<string, object>
            {
                ["@Id"]=expenses.Id,
                ["@FarmId"] = expenses.FarmId,
                ["@StageOfCultivationId"] = expenses.StageOfCultivationId,
                ["@Date"] = expenses.Date,
                ["@WagesNumber"] = expenses.WaggesNumber,
                ["@AmmountSuplies"] = expenses.AmmountSupplies,
                ["@AmmountKgCollected"] = expenses.AmmountKgCollected,
                ["@CreatedAt"] = expenses.CreatedAt,
                ["@UpdatedAt"] = expenses.UpdatedAt,
                ["@DeletedAt"] = expenses.DeletedAt,
                ["@TotalValue"] = expenses.TotalValue,
                ["@Description"] = expenses.Description,
                ["@FamiliarWages"] = expenses.FamiliarWagges,
                ["@ActivityId"] = expenses.ActivityId,
                ["@CostCenterId"] = expenses.CostCenterId,
                ["@Realizado"] = 0, 
                ["@Error"] = "" 
                
            };
            var output = _db.ExecuteStoredProcedureWithOutput(sql, parameters);
            return Convert.ToInt32(output["@Realizado"]);
        }

        public List<Expenses> GetExpensesByFarmId(Guid farmId)
        {
            var sql = "EXEC [dbo].[spGetExpensesByFarmId] @FarmId";
            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = farmId
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var expensesList = new List<Expenses>();
            foreach (DataRow row in table.Rows)
            {
                var expense = new Expenses
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    FarmId = Conv.AGuid(row["FarmId"].ToString()),
                    StageOfCultivationId = Conv.AGuid(row["StageOfCultivationId"].ToString()),
                    Date = DateTime.Parse(row["Date"].ToString()),
                    WaggesNumber = Conv.ADec(row["WagesNumber"]),
                    AmmountSupplies = Conv.ADec(row["AmmountSupplies"]),
                    AmmountKgCollected = Conv.ADec(row["AmmountKgCollected"]),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    TotalValue = Conv.ADec(row["TotalValue"]),
                    Description = row["Description"].ToString(),
                    FamiliarWagges = Conv.ADec(row["FamiliarWages"]),
                    ActivityId = Conv.AGuid(row["ActivityId"].ToString()),
                    CostCenterId = Conv.AGuid(row["CostCenterId"].ToString())
                };
                expensesList.Add(expense);
            }
            return expensesList;
        }

    }
}
