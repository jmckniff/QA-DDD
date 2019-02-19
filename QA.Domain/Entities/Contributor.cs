using System.Collections.Generic;
using QA.Domain.ValueObjects;

namespace QA.Domain.Entities
{
    public class Contributor : Member
    {
        public Reputation Reputation { get; }
        public List<Answer> Answers { get; }
        public List<Comment> Comments { get; }

        public Contributor(Name name, Reputation reputation) : base(name)
        {
            Reputation = reputation;
            Answers = new List<Answer>();
            Comments = new List<Comment>();
        }

        public int GetReputation()
        {
            return Reputation.Total;
        }
    }
}