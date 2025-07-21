using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class ProductivityReport
    {
        public Guid FarmId { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public int Orders { get; set; }
        public decimal Value { get; set; }
        public decimal Ideal { get; set; }

    }
}
