using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces
{
   public interface IFloweringRecords
    {
        int CreateFloweringRecord(FloweringRecords floweringRecord);

        List<FloweringRecords> GetfloweringRecordsByUserId(Guid UserId);
    }
}
