using System;
using QA.Domain.Enums;

namespace QA.Domain.Entities
{
    public class DownVote : Vote
    {
        public DownVote(Guid contributorId) : base(contributorId, VoteType.DownVote, 1)
        {
            
        }
    }
}