using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid Id { get; set; }
    }
}
