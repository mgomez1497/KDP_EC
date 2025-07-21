using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class CoffeeSalesRep
    {
        public int Orders { get; set; }
        public string Name { get; set; }
        public decimal KgCereza { get; set; }
        public decimal KgPergamino { get; set; }
        public decimal Percents { get; set; }
        public decimal ConvertFactor { get; set; }
        public int Year { get; set; }
        public Guid FarmId { get; set; }
    }
}
