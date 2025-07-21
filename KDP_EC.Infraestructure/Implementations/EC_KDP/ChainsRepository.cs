using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Infraestructure.DBContext;
using KDP_EC.Infraestructure.DBContext.LoadEntities;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class ChainsRepository : IChains
    {
        public List<Chains> GetChains()
        {
            throw new NotImplementedException();
        }
    }
}
