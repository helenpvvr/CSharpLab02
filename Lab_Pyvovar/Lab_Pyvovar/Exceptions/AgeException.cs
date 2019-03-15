using System;

namespace Lab_Pyvovar.Exceptions
{
    internal class AgeException : Exception
    {
        public AgeException(string message)
            : base(message)
        {

        }
    }
}
