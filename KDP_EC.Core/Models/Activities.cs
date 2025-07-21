using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Activities
    {
        public Guid Id { get; set; }
        public string ActivityName { get; set; }
        public string Description { get; set; }
        public Guid ActivityTypeId { get; set; }
        public Guid CostCenterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid DeletedAt { get; set; } 
    }
}
