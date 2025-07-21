using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Expenses
    {
        public Guid Id { get; set; }
        public Guid FarmId { get; set; }
        public Guid StageOfCultivationId { get; set; }
        public DateTime? Date { get; set; }
        public decimal WaggesNumber { get; set; }
        public decimal AmmountSupplies { get; set; }
        public decimal AmmountKgCollected { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public decimal TotalValue { get; set; }
        public string Description { get; set; }
        public decimal FamiliarWagges { get; set; }
        public Guid ActivityId { get; set; }
        public Guid CostCenterId { get; set; }


    }
}
