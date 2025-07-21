using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Chains
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ChainEarly { get; set; }
        public DateTime ChainEnd { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
