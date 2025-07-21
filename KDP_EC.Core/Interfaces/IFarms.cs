using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces
{
    public interface IFarms
    {
        List<FarmInfoViewModel> GetFarmsByPersonId(string identification);
        List<Farms> GetFarmbyIdentiAPI(string identification);
        bool CreateFarm(Farms farm);
        bool UpdateFarm(Farms farm);

        bool UpdateFarmLocation(Guid id, decimal latitude, decimal longitude, DateTime updatedAt);



    }
}
