using System;

namespace QA.Domain.Exceptions
{
    public class InvalidContributorException : Exception
    {
        public InvalidContributorException(string message) : base(message)
        {
            
        }
    }
}
