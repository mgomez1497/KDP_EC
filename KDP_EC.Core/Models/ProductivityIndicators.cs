using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class ProductivityIndicators
    {
        public decimal Productivity { get; set; }
        public decimal Yield { get; set; }
        public decimal KgValue { get; set; }
        public decimal KgPickupValue { get; set; }
        public decimal ConversionFactor { get; set; }
        public decimal performanceFactor { get; set; }
        public Guid CityId { get; set; }

    }
}
