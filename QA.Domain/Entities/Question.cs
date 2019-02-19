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
    public class Question : IEntity, IVoteable, IHappened
    {
        public Guid Id { get; }
        public string Title { get; }
        public BodyContent Body { get; }
        public Poster Poster { get; }
        public List<Vote> Votes { get; private set; }
        public DateTime HappenedDateTime { get; }
        public List<Answer> Answers { get; }
        public List<Comment> Comments { get; }
        public List<Tag> Tags { get; }

        public Question(string title, BodyContent body, Poster poster)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            Poster = poster;

            Votes = new List<Vote>();
            Answers = new List<Answer>();
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }

        public Answer AcceptedAnswer
        {
            get { return Answers.FirstOrDefault(answer => answer.IsAccepted); }
        }
    
        public void AcceptAnswer(Guid answerId, Guid contributorId)
        {
            if (contributorId != Poster.Id)
            {
                throw new InvalidContributorException("Only the question poster may accept the answer");
            }

            var answer = Answers.FirstOrDefault(x => x.Id == answerId);

            if (answer == null)
            {
                throw new InvalidAnswerException(Id, answerId);
            }

            answer.AcceptAnswer();
            
            DomainEvents.Publish(new AnswerAccepted(Id, answerId));
        }

        public void UnacceptAnswer(Guid answerId, Guid contributorId)
        {
            if (contributorId != Poster.Id)
            {
                throw new InvalidContributorException("Only the question poster may unaccept the answer");
            }

            var answer = Answers.FirstOrDefault(x => x.Id == answerId);

            if (answer == null)
            {
                throw new InvalidAnswerException(Id, answerId);
            }

            answer.UnacceptAnswer();
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

            DomainEvents.Publish(new QuestionUpVoted(Id, GetVoteCount(), contributorId));
        }

        public void DownVote(Guid contributorId)
        {
            var vote = new DownVote(contributorId);

            Votes.Add(vote);

            DomainEvents.Publish(new QuestionDownVoted(Id, GetVoteCount(), contributorId));
        }

        public void AddComment(Comment comment)
        {
            const int requiredReputation = 1000;

            if (comment.GetContributorReputation() < requiredReputation)
            {
                throw new InsufficientReputationException(
                    $@"The contributor '{comment.GetContributorId()}', does not have the required reputation to comment on the question '{Id}', 
                        current reputation: {comment.GetContributorReputation()},
                        required reputation: {requiredReputation}");
            }

            Comments.Add(comment);

            DomainEvents.Publish(new QuestionCommentAdded(Id, comment.Id, comment.GetContributorId()));
        }

        public void CommentOnAnswer(Guid answerId, Comment comment)
        {
            var answer = Answers.FirstOrDefault(a => a.Id == answerId);

            if (answer == null)
            {
                throw new InvalidAnswerException(Id, answerId);
            }

            answer.AddComment(comment);
        }

        public void UpVoteAnswer(Guid answerId, Contributor contributor)
        {
            const int requiredReputation = 1000;

            if (contributor.GetReputation() < requiredReputation)
            {
                throw new InsufficientReputationException(
                    $@"The contributor '{contributor.Id}', does not have the required reputation to up vote the answer '{answerId}', 
                        current reputation: {contributor.GetReputation()},
                        required reputation: {requiredReputation}");
            }

            var answer = Answers.FirstOrDefault(a => a.Id == answerId);

            if (answer == null)
            {
                throw new InvalidAnswerException(Id, answerId);
            }

            answer.UpVote(contributor.Id);
        }

        public void DownVoteAnswer(Guid answerId, Contributor contributor)
        {
            const int requiredReputation = 1000;

            if (contributor.GetReputation() < requiredReputation)
            {
                throw new InsufficientReputationException(
                    $@"The contributor '{contributor.Id}', does not have the required reputation to down vote the answer '{answerId}', 
                        current reputation: {contributor.GetReputation()},
                        required reputation: {requiredReputation}");
            }
        }

        public void AddAnAnswer(Answer answer)
        {
            const int requiredReputation = 1000;

            if (answer.GetContributorReputation() < requiredReputation)
            {
                throw new InsufficientReputationException(
                    $@"The contributor '{answer.GetContributorId()}', does not have the required reputation to add an answer to the question '{Id}', 
                        current reputation: {answer.GetContributorReputation()},
                        required reputation: {requiredReputation}");
            }

            Answers.Add(answer);

            DomainEvents.Publish<AnswerAdded>(new AnswerAdded(Id, answer.Id, answer.GetContributorId()));
        }
    }
}
