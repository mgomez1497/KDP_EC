using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces
{
    public interface ILots
    {
        List<Lots> GetLotsbyFarmId(Guid FarmId, Guid? TipoLote, Guid? VariedadLote, Guid? TipoRenovacion);
        List<Lots> GetLotsbyFarmIdAPI(Guid FarmId);
        int CreateLots(Lots lots);
        int UpdateLots(Lots lots);

    }
}
