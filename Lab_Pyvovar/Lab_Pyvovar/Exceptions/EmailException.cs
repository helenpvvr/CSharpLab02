using System;

namespace Lab_Pyvovar.Exceptions
{
    internal class EmailException : Exception
    {
        public EmailException(string message)
            : base(message)
        {

        }
    }
}
