using System;

namespace QA.Domain.Exceptions
{
    public class InsufficientReputationException : Exception
    {
        public InsufficientReputationException(string message) : base(message)
        {
            
        }
    }
}
