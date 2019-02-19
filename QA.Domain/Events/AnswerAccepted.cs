using System;

namespace QA.Domain.Events
{
    public class AnswerAccepted : IEvent
    {
        public Guid QuestionId { get; }
        public Guid AnswerId { get; }
        public DateTime DateTimeStamp { get; }

        public AnswerAccepted(Guid questionId, Guid answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
            DateTimeStamp = DateTime.Now;
        }
    }
}
