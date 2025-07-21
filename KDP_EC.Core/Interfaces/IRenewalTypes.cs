using KDP_EC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public interface IRenewal_Types
    {
        List<Renewal_Types> GetRenewalTypes();
    }
}
