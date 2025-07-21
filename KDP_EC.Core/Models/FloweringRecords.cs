using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class FloweringRecords
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public DateTime floweringDate { get; set; }
        public Guid Department { get; set; }
        public Guid Municipality { get; set; }
        public Guid? Village { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string elevation { get; set; }
        public string floweringType { get; set; }
        public string? file { get; set; }
        public Guid User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
