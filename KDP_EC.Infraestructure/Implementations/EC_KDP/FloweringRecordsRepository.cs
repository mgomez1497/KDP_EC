using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class FloweringRecordsRepository:IFloweringRecords
    {
        private readonly SqlDbManager _db;

        public FloweringRecordsRepository(SqlDbManager db)
        {
            _db = db;
        }

        public int CreateFloweringRecord(FloweringRecords floweringRecord)
        {
            var sql = "Exec [dbo].[spCreateFloweringRecords]" +
                "@id,@floweringDate," +
                "@deparment," +
                "@municipality," +
                "@village," +
                "@latitude," +
                "@longitude," +
                "@elevation," +
                "@floweringType," +
                "@file," +
                "@User," +
                "@createdAt," +
                "@updatedAt," +
                "@deletedAt," +
                "@Realizado OUTPUT," +
                "@Error OUTPUT";

            var parameters = new Dictionary<string, object>
            {
                ["@id"] = floweringRecord.Id,
                ["@floweringDate"] = floweringRecord.floweringDate,
                ["@deparment"] = floweringRecord.Department,
                ["@municipality"] = floweringRecord.Municipality,
                ["@village"] = floweringRecord.Village,
                ["@latitude"] = floweringRecord.latitude,
                ["@longitude"] = floweringRecord.longitude,
                ["@elevation"] = floweringRecord.elevation,
                ["@floweringType"] = floweringRecord.floweringType,
                ["@file"] = floweringRecord.file,
                ["@User"] = floweringRecord.User,
                ["@createdAt"] = floweringRecord.CreatedAt,
                ["@updatedAt"] = floweringRecord.UpdatedAt,
                ["@deletedAt"] = floweringRecord.DeletedAt,
                ["@Realizado"] = 0,
                ["@Error"] = ""
            };

            var output=_db.ExecuteStoredProcedureWithOutput(sql, parameters);

            return Convert.ToInt32(output["@Realizado"]);
        }

        public List<FloweringRecords> GetfloweringRecordsByUserId(Guid UserId)
        {
            var sql = @"Exec [dbo].[spGetfloweringRecordsByUserId] @UserId";
            var parameters = new Dictionary<string, object>
            {
                ["@UserId"] = UserId
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var floweringList = new List<FloweringRecords>();

            foreach(DataRow row in table.Rows)
            {
                var flowering = new FloweringRecords
                {
                    Id = Conv.AGuid(row["Id"]),
                    floweringDate = Conv.AFecha(row["floweringDate"]),
                    Department = Conv.AGuid(row["Deparment"]),
                    Municipality = Conv.AGuid(row["Municipality"]),
                    Village = Conv.AGuid(row["Village"]),
                    latitude = row["latitude"].ToString(),
                    longitude = row["longitude"].ToString(),
                    elevation = row["elevation"].ToString(),
                    floweringType = row["floweringType"].ToString(),
                    file = row["file"].ToString(),
                    User = Conv.AGuid(row["User"]),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt= Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt= Conv.AFecha(row["DeletedAt"])
                };

                floweringList.Add(flowering);
            }
            return floweringList;


        }

    }
}
