using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Interfaces;
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
    public class ExportTecnRepository : IExport_Tecnician
    {
        private readonly SqlDbManager _db;

        public ExportTecnRepository(SqlDbManager db)
        {
            _db = db;
        }

        public List<ExportTecnViewModel> GetTecnicianbyExport(Guid ExpId)
        {
            var sql = "EXEC [dbo].[spGetTecnicianbyExport] @ExpId";
            var parameters = new Dictionary<string, object>
            {
                ["@ExpId"] = ExpId
            };
            var table = _db.ExecuteQuery(sql, parameters);
            var exportTecn = new List<ExportTecnViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var tecn = new ExportTecnViewModel
                {
                    TecId= Conv.AGuid(row["TecId"].ToString()),
                    Identification = row["Identification"].ToString(),
                    NombreCompleto = row["NombreCompleto"].ToString(),
                    FincasAsignadas = Conv.AEntero(row["FincasAsignadas"].ToString())

                };
                exportTecn.Add(tecn);
            }

            return exportTecn;
        }

       


    }
}
