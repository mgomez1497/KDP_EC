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
    public class Lots_VarietysRepository : ILots_Varietys
    {
        private readonly SqlDbManager _db;

        public Lots_VarietysRepository(SqlDbManager db)
        {
            _db = db;
        }
        public List<Lots_Varietys> GetLots_Varietys()
        {
            var sql = "EXEC [dbo].[GetLotsVarietys]";
            var table = _db.ExecuteQuery(sql, null);
            var lotsVarietys = new List<Lots_Varietys>();

            foreach (DataRow row in table.Rows)
            {
                var lotsVariety = new Lots_Varietys
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    Status = string.IsNullOrWhiteSpace(row["Status"].ToString())
                };
                lotsVarietys.Add(lotsVariety);
            }
            return lotsVarietys;

        }
    }
    
}
