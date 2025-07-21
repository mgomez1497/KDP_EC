using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.ModelView
{
    public class FarmInfoViewModel
    {
       
        public string NombreFinca { get; set; }
        public string Code { get; set; }
        public string HASL { get; set; }
        public string Vereda { get; set; }
        public decimal Ha_Totales { get; set; }
        public decimal Ha_Cafe { get; set; }
        public int TotalFincas { get; set; }
        public Guid FarmId { get; set; }

    }
}
