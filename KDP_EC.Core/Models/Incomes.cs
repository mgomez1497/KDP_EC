using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Incomes
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalValue { get; set; }
        public decimal KgSold { get; set; }
        public decimal HealthyAlmondPercent { get; set; }
        public decimal PoorCoffeePercent { get; set; }
        public decimal DecreasePercent { get; set; }
        public decimal PerformanceFactor { get; set; }
        public Guid FarmId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public decimal PercentageKg{ get; set; }
        public string Invoice { get; set; }
    }
}
