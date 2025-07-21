using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Person_Farm
    {
        public Guid Id_Farm { get; set; }
        public Guid Id_Person { get; set; }
        public Guid Id_Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String? FamilyBond { get; set; }

    }
}
