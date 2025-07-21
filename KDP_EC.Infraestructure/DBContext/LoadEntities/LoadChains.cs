using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.DBContext.LoadEntities
{
    public class LoadChains
    {
        public static List<Chains> GetChains(DataTable dt)
        {
            List<Chains> lista = new List<Chains>();
            foreach (DataRow rw in dt.Rows)
            {
                lista.Add(GetChain(rw));
            }
            return lista;
        }

        public static Chains GetChain(DataRow rw)
        {
            if (rw == null)
            {
                throw new ArgumentNullException(nameof(rw), "DataRow cannot be null");
            }

            Chains chain = new Chains
            {
                Id = Conv.AGuid(rw["Id"]),
                Name = Conv.AStr(rw["Name"]),
                ChainEarly = Conv.AFecha(rw["ChainEarly"]),
                ChainEnd = Conv.AFecha(rw["ChainEnd"]),
                CompanyId = Conv.AGuid(rw["CompanyId"]),
                Status = Conv.ABool(rw["Status"]),
                CreatedAt = Conv.AFecha(rw["CreatedAt"]),
                UpdatedAt = Conv.AFecha(rw["UpdatedAt"]),
                DeletedAt = Conv.AFecha(rw["DeletedAt"])

            };
            return chain;
        }
    }


}
