using QA.Domain;
using QA.Domain.Entities;
using QA.Domain.Events;
using QA.Domain.Exceptions;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Questions
{
    public class QuestionAcceptAnswerTests
    {
        [Fact]
        public void WhenThePosterAcceptsAnAsnwer_TheAnswer_ShouldBeMarkedAsAccepted()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(1000));  
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            question.AddAnAnswer(answer);

            var isAccepted = false;

            DomainEvents.ListenFor<AnswerAccepted>(@event =>
            {
                isAccepted = true;
            });

            question.AcceptAnswer(answer.Id, poster.Id);

            Assert.True(isAccepted);
            Assert.NotNull(question.AcceptedAnswer);
            Assert.Equal(answer, question.AcceptedAnswer);
        }

        [Fact]
        public void WhenAContributorAcceptsAnAnswer_AnException_ShouldBeThrown()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(1000));
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            question.AddAnAnswer(answer);

            Assert.Throws<InvalidContributorException>(() => question.AcceptAnswer(answer.Id, contributor.Id));
        }

        [Fact]
        public void WhenAcceptingAnAnswer_ThatDoesNotExistAnException_ShouldBeThrown()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(100));
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            Assert.Throws<InvalidAnswerException>(() => question.AcceptAnswer(answer.Id, poster.Id));
        }
    }
}
