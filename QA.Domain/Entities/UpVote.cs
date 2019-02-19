using System;
using QA.Domain.Enums;

namespace QA.Domain.Entities
{
    public class UpVote : Vote
    {
        public UpVote(Guid contributorId) : base(contributorId, VoteType.UpVote, 1)
        {

        }
    }
}