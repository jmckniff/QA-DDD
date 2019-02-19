using System;
using System.Collections.Generic;
using QA.Domain.Entities;

namespace QA.Domain.Interfaces
{
    public interface IVoteable
    {
        List<Vote> Votes { get; }
        int GetVoteCount();
        void UpVote(Guid contributorId);
        void DownVote(Guid contributorId);
    }
}