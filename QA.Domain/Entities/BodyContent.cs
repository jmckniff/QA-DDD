using System;
using QA.Domain.Enums;
using QA.Domain.Interfaces;

namespace QA.Domain.Entities
{
    public class BodyContent : IEntity
    {
        public Guid Id { get; }
        public BodyContentType ContentType { get; }
        public string Content { get; }

        public BodyContent(BodyContentType contentType, string content)
        {
            Id = Guid.NewGuid();
            ContentType = contentType;
            Content = content;
        }
    }
}