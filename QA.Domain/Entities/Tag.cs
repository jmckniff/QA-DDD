using System;
using QA.Domain.Interfaces;

namespace QA.Domain.Entities
{
    public class Tag : IEntity
    {
        public Guid Id { get; }
        public string Name { get; }

        public Tag(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        
    }
}