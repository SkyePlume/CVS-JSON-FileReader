using Medica.Uk.TechnicalDemonstration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medica.Uk.TechnicalDemonstration.Logic
{
    public interface IReadFromInterface 
    {
        List<CustomerRecord> FindRecords(string path);
    }
}
