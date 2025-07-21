using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class Person
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid CountryId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } 

        public DateTime? DeletedAt { get; set; }

        public string Gender { get; set; }

        public string IdentificationType { get; set; }
    }
}
