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
    public class LotsRepository : ILots
    {
        private readonly SqlDbManager _db;

        public LotsRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<Lots> GetLotsbyFarmId(Guid farmId, Guid? tipoLote = null, Guid? variedadLote = null, Guid? tipoRenovacion = null)
        {
            var sql = "EXEC [dbo].[spGetLotsByFarmId] @FarmId, @TipoLote, @VariedadLote, @TipoRenovacion";

            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = farmId,
                ["@TipoLote"] = (object?)tipoLote ?? DBNull.Value,
                ["@VariedadLote"] = (object?)variedadLote ?? DBNull.Value,
                ["@TipoRenovacion"] = (object?)tipoRenovacion ?? DBNull.Value
            };

            var table = _db.ExecuteQuery(sql, parameters);
            var lots = new List<Lots>();

            foreach (DataRow row in table.Rows)
            {
                var lot = new Lots
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    FarmId = Guid.Parse(row["FarmId"].ToString()),
                    LotName = row["LotName"].ToString(),
                    VarietyId = Conv.AGuid(Convert.ToString(row["VarietyId"])),
                    HA = Conv.ADec(row["HA"]),
                    WorkDate = Conv.AFecha(row["WorkDate"]),
                    TreesDistance = Conv.ADec(row["TreesDistance"]),
                    GrooveDistance = Conv.ADec(row["GrooveDistance"]),
                    Density = Conv.ADec(row["Density"]),
                    TreesNumber = Conv.ADec(row["TreesNumber"]),
                    StemsByPlants = Conv.AEntero(row["StemsByPlants"]),
                    TypeReknewalId = Conv.AGuid(Convert.ToString(row["TypeReknewalId"])),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    TypeLotId = Conv.AGuid(Convert.ToString(row["TypeLotId"])),
                    TotalStems = Conv.ADec(row["TotalStems"]),
                    Latitude = row["Latitude"].ToString(),
                    Longitude = row["Longitude"].ToString()
                };
                lots.Add(lot);
            }

            return lots;
        }

        public List<Lots> GetLotsbyFarmIdAPI(Guid farmId)
        {
            var sql = "EXEC [dbo].[spGetLotsbyFarmIdAPI] @FarmId";
            var parameters = new Dictionary<string, object>
            {
                ["@FarmId"] = farmId
            };

            var table = _db.ExecuteQuery(sql, parameters);
            var lots = new List<Lots>();

            foreach (DataRow row in table.Rows)
            {
                var lot = new Lots
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    FarmId = Guid.Parse(row["FarmId"].ToString()),
                    LotName = row["LotName"].ToString(),
                    VarietyId = Conv.AGuid(Convert.ToString(row["VarietyId"])),
                    HA = Conv.ADec(row["HA"]),
                    WorkDate = Conv.AFecha(row["WorkDate"]),
                    TreesDistance = Conv.ADec(row["TreesDistance"]),
                    GrooveDistance = Conv.ADec(row["GrooveDistance"]),
                    Density = Conv.ADec(row["Density"]),
                    TreesNumber = Conv.ADec(row["TreesNumber"]),
                    StemsByPlants = Conv.AEntero(row["StemsByPlants"]),
                    TypeReknewalId = Conv.AGuid(Convert.ToString(row["TypeReknewalId"])),
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    TypeLotId = Conv.AGuid(Convert.ToString(row["TypeLotId"])),
                    TotalStems = Conv.ADec(row["TotalStems"]),
                    Latitude = row["Latitude"].ToString(),
                    Longitude = row["Longitude"].ToString()
                };
                lots.Add(lot);
            }
            return lots;
        }



        public int CreateLots(Lots lots)
        {
            var sql = @"EXEC [dbo].[spCreateLots] 
                        @Id,
                        @FarmId,
                        @LotName,
                        @VarietyId,
                        @HA,
                        @WorkDate,
                        @TreesDistance,
                        @GrooveDistance,
                        @Density,
                        @TreesNumber,
                        @TypeReknewalId,
                        @StemsByPlants,
                        @CreatedAt,
                        @UpdatedAt,
                        @DeletedAt,
                        @TypeLotId,
                        @TotalStems,
                        @Latitude,
                        @Longitude,
                        @Realizado OUTPUT,
                        @Error OUTPUT";

            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = lots.Id,
                ["@FarmId"] = lots.FarmId,
                ["@LotName"] = lots.LotName,
                ["@VarietyId"] = lots.VarietyId,
                ["@HA"] = lots.HA,
                ["@WorkDate"] = lots.WorkDate,
                ["@TreesDistance"] = lots.TreesDistance,
                ["@GrooveDistance"] = lots.GrooveDistance,
                ["@Density"] = lots.Density,
                ["@TreesNumber"] = lots.TreesNumber,
                ["@TypeReknewalId"] = lots.TypeReknewalId,
                ["@StemsByPlants"] = lots.StemsByPlants,
                ["@CreatedAt"] = DateTime.Now,
                ["@UpdatedAt"] = DateTime.Now,
                ["@DeletedAt"] = DBNull.Value,
                ["@TypeLotId"] = lots.TypeLotId,
                ["@TotalStems"] = lots.TotalStems,
                ["@Latitude"] = lots.Latitude,
                ["@Longitude"] = lots.Longitude,
                ["@Realizado"] = 0,
                ["@Error"] = ""      
            };

            
            var output = _db.ExecuteStoredProcedureWithOutput(sql, parameters);

            
            return Convert.ToInt32(output["@Realizado"]);
        }

        public int UpdateLots(Lots lots)
        {
            var sql = "EXEC [dbo].[spUpdateLots] @Id, @FarmId, @LotName, @VarietyId, @HA, @WorkDate, @TreesDistance, @GrooveDistance, @Density, @TreesNumber, @TypeReknewalId, @StemsByPlants, @CreatedAt, @UpdatedAt, @DeletedAt, @TypeLotId, @TotalStems, @Latitude, @Longitude";
            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = lots.Id,
                ["@FarmId"] = lots.FarmId,
                ["@LotName"] = lots.LotName,
                ["@VarietyId"] = lots.VarietyId,
                ["@HA"] = lots.HA,
                ["@WorkDate"] = lots.WorkDate,
                ["@TreesDistance"] = lots.TreesDistance,
                ["@GrooveDistance"] = lots.GrooveDistance,
                ["@Density"] = lots.Density,
                ["@TreesNumber"] = lots.TreesNumber,
                ["@TypeReknewalId"] = lots.TypeReknewalId,
                ["@StemsByPlants"] = lots.StemsByPlants,
                ["@CreatedAt"] = DateTime.Now,
                ["@UpdatedAt"] = DateTime.Now,
                ["@DeletedAt"] = null,
                ["@TypeLotId"] = lots.TypeLotId,
                ["@TotalStems"] = lots.TotalStems,
                ["@Latitude"] = lots.Latitude,
                ["@Longitude"] = lots.Longitude
            };
            return _db.ExecuteNonQuery(sql, parameters);
        }
    }
}
