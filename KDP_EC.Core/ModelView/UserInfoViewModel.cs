using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.ModelView
{
    public class UserInfoViewModel
    {
        public string Identification { get; set; }
        public string NombreCompleto { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public Guid Id_Rol { get; set; } 
        public Guid Id_Company { get; set; }
    }
}
