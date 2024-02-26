using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medica.Uk.TechnicalDemonstration.Exceptions
{
    public class CSVException : Exception
    {
        public CSVException(string message) : base(message) { }
    }
}
