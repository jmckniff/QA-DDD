using System;

namespace QA.Domain.Events
{
    public class AnswerCommentAdded : IEvent
    {
        public Guid AnswerId { get; }
        public Guid CommentId { get; }
        public Guid ContributorId { get; }
        public DateTime DateTimeStamp { get; }

        public AnswerCommentAdded(Guid answerId, Guid commentId, Guid contributorId)
        {
            AnswerId = answerId;
            CommentId = commentId;
            ContributorId = contributorId;
            DateTimeStamp = DateTime.Now;
        }
    }
}
