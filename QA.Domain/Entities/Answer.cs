using System;
using System.Collections.Generic;
using System.Linq;
using QA.Domain.Enums;
using QA.Domain.Events;
using QA.Domain.Exceptions;
using QA.Domain.Interfaces;
using QA.Framework;

namespace QA.Domain.Entities
{
    public class Answer : IEntity, IVoteable, IHappened
    {
        public Guid Id { get; }
        public Guid QuestionId { get; }
        public Contributor Contributor { get; }
        public BodyContent Body { get; }
        public List<Vote> Votes { get; private set; }
        public List<Comment> Comments { get; set; }
        public DateTime HappenedDateTime { get; }
        public bool IsAccepted { get; private set; }

        public Answer(Guid questionId, Contributor contributor, BodyContent body)
        {
            Id = Guid.NewGuid();
            QuestionId = questionId;
            Contributor = contributor;
            Body = body;
            HappenedDateTime = DateTime.Now;
            Votes = new List<Vote>();
            Comments = new List<Comment>();
        }

        public int GetVoteCount()
        {
            var upVotes = Votes.Count(vote => vote.Type == VoteType.UpVote);
            var downVotes = Votes.Count(vote => vote.Type == VoteType.DownVote);

            return upVotes - downVotes;
        }

        public void UpVote(Guid contributorId)
        {
            var vote = new UpVote(contributorId);

            Votes.Add(vote);
        }

        public void DownVote(Guid contributorId)
        {
            var vote = new DownVote(contributorId);

            Votes.Add(vote);
        }

        public void AcceptAnswer()
        {
            IsAccepted = true;

            DomainEvents.Publish(new AnswerAccepted(QuestionId, Id));
        }

        public void UnacceptAnswer()
        {
            IsAccepted = false;

            DomainEvents.Publish(new AnswerUnaccepted(QuestionId, Id));
        }

        public void AddComment(Comment comment)
        {
            const int requiredReputation = 1000;

            if (comment.GetContributorReputation() < requiredReputation)
            {
                throw new InsufficientReputationException(
                    $@"The contributor '{comment.GetContributorId()}', does not have the required reputation to upvote the answer '{Id}', 
                        current reputation: {comment.GetContributorReputation()},
                        required reputation: {requiredReputation}");
            }

            Comments.Add(comment);

            DomainEvents.Publish(new AnswerCommentAdded(Id, comment.Id, comment.GetContributorId()));
        }

        public Guid GetContributorId()
        {
            return Contributor.Id;
        }

        public int GetContributorReputation()
        {
            return Contributor.Reputation.Total;
        }
        
    }

    
}