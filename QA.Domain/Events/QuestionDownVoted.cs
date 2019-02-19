using System;

namespace QA.Domain.Events
{
    public class QuestionDownVoted : IEvent

    {
        public Guid ContributorId { get; }
        public int TotalVotes { get; }
        public Guid QuestionId { get; }
        public DateTime DateTimeStamp { get; }

        public QuestionDownVoted(Guid questionId, int totalVotes, Guid contributorId)
        {
            QuestionId = questionId;
            TotalVotes = totalVotes;
            ContributorId = contributorId;
            DateTimeStamp = DateTime.Now;
        }
    }
}
