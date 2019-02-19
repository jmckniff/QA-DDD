using System;

namespace QA.Domain.Events
{
    public class QuestionCommentAdded : IEvent
    {
        public Guid QuestionId { get; }
        public Guid CommentId { get; }
        public Guid ContributorId { get; }
        public DateTime DateTimeStamp { get; }

        public QuestionCommentAdded(Guid questionId, Guid commentId, Guid contributorId)
        {
            QuestionId = questionId;
            CommentId = commentId;
            ContributorId = contributorId;
            DateTimeStamp = DateTime.Now;
        }
    }
}
