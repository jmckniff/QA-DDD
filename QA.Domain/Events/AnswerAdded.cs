using System;

namespace QA.Domain.Events
{
    public class AnswerAdded : IEvent
    {
        public Guid QuestionId { get; }
        public Guid AnswerId { get; }
        public Guid ContributorId { get; }
        public DateTime DateTimeStamp { get; }

        public AnswerAdded(Guid questionId, Guid answerId, Guid contributorId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
            ContributorId = contributorId;
            DateTimeStamp = DateTime.Now;
        }
    }
}
