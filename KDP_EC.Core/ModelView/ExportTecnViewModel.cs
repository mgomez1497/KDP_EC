using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.ModelView
{
    public class ExportTecnViewModel
    {
        public Guid TecId { get; set; }
        public string Identification { get; set; }
        public string NombreCompleto { get; set; }
        public int FincasAsignadas { get; set; }

    }
}
