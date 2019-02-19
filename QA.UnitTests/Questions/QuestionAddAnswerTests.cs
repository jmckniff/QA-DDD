using QA.Domain;
using QA.Domain.Entities;
using QA.Domain.Events;
using QA.Domain.Exceptions;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Questions
{
    public class QuestionAddAnswerTests
    {
        [Fact]
        public void WhenAContributorAddsAnAsnwer_TheAnswer_ShouldBeAdded()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(1000));  
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            var isAdded = false;

            DomainEvents.ListenFor<AnswerAdded>(@event =>
            {
                isAdded = true;
            });

            question.AddAnAnswer(answer);
            
            Assert.True(isAdded);
            Assert.Equal(1, question.Answers.Count);
        }
        
        [Fact]
        public void WhenAContributorAddsAnAnswer_WithInsufficientReputation_AnExceptionShouldBeThrown()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(100));
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            Assert.Throws<InsufficientReputationException>(() => question.AddAnAnswer(answer));
        }
    }
}
