using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Exceptions
{
    public class UserIsInactiveException : Exception
    {
        public UserIsInactiveException() : base("The user is inactive")
        {

        }
    }
}
