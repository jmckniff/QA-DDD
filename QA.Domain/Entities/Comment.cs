using System;
using System.Collections.Generic;
using System.Linq;
using QA.Domain.Enums;
using QA.Domain.Interfaces;
using QA.Framework;

namespace QA.Domain.Entities
{
    public class Comment : IEntity, IVoteable, IHappened
    {
        public Guid Id { get; }
        public Contributor Contributor { get; }
        public string Text { get; }
        public List<Vote> Votes { get; private set; }
        public DateTime HappenedDateTime { get; }
        public Comment(Contributor contributor, string text)
        {
            Contributor = contributor;
            Text = text;
            HappenedDateTime = DateTime.Now;
        }
        
        public int GetContributorReputation()
        {
            return Contributor.GetReputation();
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

        public Guid GetContributorId()
        {
            return Contributor.Id;
        }
    }
}