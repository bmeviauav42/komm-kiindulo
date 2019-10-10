using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msa.Comm.Lab.Services.Catalog.Exceptions
{
    public class TestTransientException : Exception
    {
        public TestTransientException() : base()
        {
        }

        public TestTransientException(string message) : base(message)
        {
        }

        public TestTransientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
