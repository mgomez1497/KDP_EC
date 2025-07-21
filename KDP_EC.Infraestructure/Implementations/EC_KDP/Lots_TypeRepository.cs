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
    public class Lots_TypeRepository: ILots_Type
    {
        private readonly SqlDbManager _db;

        public Lots_TypeRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Lots_Type> GetLotsType()
        {
            var sql = "EXEC [dbo].[spGetLotsTypes]";
            var table = _db.ExecuteQuery(sql, null);
            var lotsTypes = new List<Lots_Type>();

            foreach (DataRow row in table.Rows)
            {
                var lotsType = new Lots_Type
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    Status = Conv.ABool(row["Status"].ToString())
                };
                lotsTypes.Add(lotsType);
            }

            return lotsTypes;
        }

       
    }
}
