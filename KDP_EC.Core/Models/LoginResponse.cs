using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
