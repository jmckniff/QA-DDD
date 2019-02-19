using System;
using QA.Domain.Enums;
using QA.Domain.Interfaces;
using QA.Framework;

namespace QA.Domain.Entities
{
    public class Vote : IEntity, IHappened
    {
        public Guid Id { get; }
        public VoteType Type { get; }
        public int Value { get; }
        public Guid ContributorId { get; }
        public DateTime HappenedDateTime { get; }

        public Vote(Guid contributorId, VoteType voteType, int value)
        {
            ContributorId = contributorId;
            Type = voteType;
            Value = value;
            HappenedDateTime = DateTime.Now;
        }
    }
}