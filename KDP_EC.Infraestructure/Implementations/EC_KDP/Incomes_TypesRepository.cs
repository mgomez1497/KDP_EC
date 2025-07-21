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
    public class Incomes_TypesRepository:IIncomesTypes
    {
        private readonly SqlDbManager _db;

        public Incomes_TypesRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<IncomesTypes> GetIncomesTypes()
        {
                var sql = "EXEC [dbo].[spGetIncomes_Types]";
                var table = _db.ExecuteQuery(sql, null);
                var incomesTypes = new List<IncomesTypes>();
                foreach (DataRow row in table.Rows)
                {
                    var incomeType = new IncomesTypes
                    {
                        Id = Conv.AGuid(row["Id"]),
                        Name = row["Name"].ToString(),
                        CreatedAt = Conv.AFecha(row["CreatedAt"]),
                        UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                        DeletedAt = Conv.AFecha(row["DeletedAt"]),
                        Status = Conv.ABool(row["Status"])
                    };
                    incomesTypes.Add(incomeType);
                }
                return incomesTypes;
        }
    }
}
