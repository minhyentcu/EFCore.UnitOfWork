using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.UnitOfWork.Exception
{
    public class UnitOfWorkException : System.Exception
    {
        public UnitOfWorkException(string message) : base(message) { }
        public UnitOfWorkException(string message, System.Exception inner) : base(message, inner) { }
    }
}
