using System;

namespace Lab_Pyvovar.Exceptions
{
    internal class NameException : Exception
    {
        public NameException(string message)
            : base(message)
        {

        }
    }
}
