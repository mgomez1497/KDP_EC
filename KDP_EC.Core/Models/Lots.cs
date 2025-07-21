using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Lots
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public Guid FarmId { get; set; }
        public string LotName { get; set; }
        public Guid VarietyId { get; set; }
        public decimal HA { get; set; }
        public DateTime? WorkDate { get; set; }
        public decimal TreesDistance { get; set; }
        public decimal GrooveDistance { get; set; }
        public decimal Density { get; set; }
        public decimal TreesNumber { get; set; }
        public Guid TypeReknewalId { get; set; }
        public int StemsByPlants { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid TypeLotId { get; set; }
        public decimal TotalStems { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}
