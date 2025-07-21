using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class BalanceCostCenters
    {
        public Guid CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public decimal FarmAverage { get; set; }
        public decimal ExportAverage { get; set; }
    }
}
