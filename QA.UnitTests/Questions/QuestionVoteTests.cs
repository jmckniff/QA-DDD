using System;
using QA.Domain;
using QA.Domain.Entities;
using QA.Domain.Events;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Questions
{
    public class QuestionVoteTests
    {
        [Fact]
        public void WhenDownVoted_TheVotesCount_ShouldDecreaseByOne()
        {
            var hasVoteDecreased = false;

            DomainEvents.ListenFor<QuestionDownVoted>(@event =>
            {
                hasVoteDecreased = true;
            });

            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            question.DownVote(Guid.NewGuid());

            Assert.Equal(-1, question.GetVoteCount());
            Assert.True(hasVoteDecreased);
        }

        [Fact]
        public void WhenUpVoted_TheVotesCount_ShouldIncreaseByOne()
        {
            var hasVoteIncreased = false;

            DomainEvents.ListenFor<QuestionUpVoted>(@event =>
            {
                hasVoteIncreased = true;
            });

            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            question.UpVote(Guid.NewGuid());

            Assert.Equal(1, question.GetVoteCount());
            Assert.True(hasVoteIncreased);
        }
    }
}
