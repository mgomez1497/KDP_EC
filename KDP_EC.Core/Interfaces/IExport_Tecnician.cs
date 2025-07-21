using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces
{
    public interface IExport_Tecnician
    {
        List<ExportTecnViewModel> GetTecnicianbyExport(Guid ExpId);
    }
}
