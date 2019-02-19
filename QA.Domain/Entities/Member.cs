using System;
using QA.Domain.Interfaces;
using QA.Domain.ValueObjects;

namespace QA.Domain.Entities
{
    public class Member : IEntity
    { 
        public Guid Id { get; }
        public Name Name { get; }

        public Member(Name name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}