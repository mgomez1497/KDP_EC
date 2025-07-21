    using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class FarmsRepository: IFarms
    {
        private readonly SqlDbManager _db;

        public FarmsRepository(SqlDbManager db)
        {
            _db = db;
        }
        
        public List<FarmInfoViewModel> GetFarmsByPersonId(string identification)
        {
            var sql = "EXEC [dbo].[spGetFarmsByPersonId] @Identification";
            var parameters = new Dictionary<string, object>
            {
                ["@Identification"] = identification
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var farms = new List<FarmInfoViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var farm = new FarmInfoViewModel
                {
                    NombreFinca = row["NombreFinca"].ToString(),
                    Code = row["Code"].ToString(),
                    HASL = row["HASL"].ToString(),
                    Vereda = row["Vereda"].ToString(),
                    Ha_Totales= Conv.ADec(row["Ha_Totales"].ToString()),
                    Ha_Cafe = Conv.ADec(row["Ha_Cafe"].ToString()),
                    TotalFincas = Conv.AEntero(row["TotalFincas"].ToString()),
                    FarmId = Conv.AGuid(row["FarmId"].ToString())
                };
                farms.Add(farm);
            }
            return farms;

        }

        public List<Farms> GetFarmbyIdentiAPI(string identification)
        {
            var sql = "EXEC [dbo].[spGetFarmbyIdentiAPI] @PersonId";
            var parameters = new Dictionary<string, object>
            {
                ["@PersonId"] = identification
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var farms = new List<Farms>();
            foreach (DataRow row in table.Rows)
            {
                var farm = new Farms
                {
                    Id = Conv.AGuid(row["Id"].ToString()),
                    Name = row["Name"].ToString(),
                    HASL = Conv.ADec(row["HASL"].ToString()),
                    Code = row["Code"].ToString(),
                    VillageId = Conv.AGuid(row["VillageId"].ToString()),
                    Latitude = Conv.ADec(row["Latitude"].ToString()),
                    Longitude = Conv.ADec(row["Longitude"].ToString()),
                    CreatedAt = Conv.AFecha(row["CreatedAt"].ToString()),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"].ToString()),
                    DeletedAt = Conv.AFecha(row["DeletedAt"].ToString()),
                    CompanyId= Conv.AGuid(row["CompanyId"].ToString()),
                    FLP_ID = row["FLP_ID"].ToString(),
                    StatusFLP = row["StatusFLP"].ToString(),
                    TotalAreaHa = Conv.ADec(row["TotalAreaHa"].ToString()),
                    CoffeeAreaHa = Conv.ADec(row["CoffeeAreaHa"].ToString()),
                    Productivity = Conv.ADec(row["Productivity"].ToString()),
                    FLP_ID_Consecutivo = Conv.AEntero(row["FLP_ID_Consecutivo"].ToString())

                };
                farms.Add(farm);
            }
            return farms;
        }

        public bool CreateFarm(Farms farm)
        {
            return false;
        }

        public bool UpdateFarm(Farms farm)
        {
            return false;
        }

        public bool UpdateFarmLocation(Guid id, decimal latitude, decimal longitude, DateTime updatedAt)
        {
            var sql = "EXEC [dbo].[spUpdateFarmByIdApi] @Id, @Latitude,@Longitude,@UpdatedAt";
            var parameters = new Dictionary<string, object>
            {
                ["@Id"] = id,
                ["@Latitude"] = latitude,
                ["@Longitude"] = longitude,
                ["@UpdatedAt"] = updatedAt
            };

            try
            {
                _db.ExecuteQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error actualizando ubicación de la finca: " + ex.Message);
                return false;
            }
        }
    }
}
