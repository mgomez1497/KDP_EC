using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces
{
   public interface IVillages
    {
        List<Villages> GetVillagesbyId(Guid id);
        List<Villages> GetVillages();
        
    }
}
