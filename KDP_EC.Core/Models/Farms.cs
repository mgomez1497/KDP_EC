using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    
    public class Farms
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string FLP_ID { get; set; }
        public string StatusFLP { get; set; }
        public string Name { get; set; }
        public decimal? HASL { get; set; }
        public string Code { get; set; }
        public Guid VillageId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal TotalAreaHa { get; set; }
        public decimal CoffeeAreaHa { get; set; }
        public decimal Productivity { get; set; }
        public int FLP_ID_Consecutivo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public Guid CompanyId { get; set; }
    }
}
