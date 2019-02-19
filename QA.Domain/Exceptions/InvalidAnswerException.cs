using System;

namespace QA.Domain.Exceptions
{
    public class InvalidAnswerException : Exception
    {
        public InvalidAnswerException(Guid questionId, Guid answerId) : 
            base($"The question with id '{questionId}' does not have an answer with the id '{answerId}'")
        {
            
        }
    }
}
