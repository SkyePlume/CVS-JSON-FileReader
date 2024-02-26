using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medica.Uk.TechnicalDemonstration.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message) { }
    }
}
