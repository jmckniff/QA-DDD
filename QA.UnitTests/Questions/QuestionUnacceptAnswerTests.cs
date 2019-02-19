using QA.Domain;
using QA.Domain.Entities;
using QA.Domain.Events;
using QA.Domain.Exceptions;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Questions
{
    public class QuestionUnacceptAnswerTests
    {
        [Fact]
        public void WhenThePosterUnacceptsAnAsnwer_TheAnswer_ShouldBeMarkedAsNotAccepted()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(1000));  
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            question.AddAnAnswer(answer);
            question.AcceptAnswer(answer.Id, poster.Id);

            var isUnaccepted = false;

            DomainEvents.ListenFor<AnswerUnaccepted>(@event =>
            {
                isUnaccepted = true;
            });

            question.UnacceptAnswer(answer.Id, poster.Id);

            Assert.True(isUnaccepted);
            Assert.Null(question.AcceptedAnswer);
        }

        [Fact]
        public void WhenAContributorThatIsNotThePoster_UnacceptsAnAnswerAnException_ShouldBeThrown()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(1000));
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            question.AddAnAnswer(answer);

            Assert.Throws<InvalidContributorException>(() => question.UnacceptAnswer(answer.Id, contributor.Id));
        }

        [Fact]
        public void WhenUnacceptingAnAnswer_ThatDoesNotExistAnException_ShouldBeThrown()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(100));
            var answer = new Answer(question.Id, contributor, new PlainTextContent("This is an answer"));

            Assert.Throws<InvalidAnswerException>(() => question.UnacceptAnswer(answer.Id, poster.Id));
        }
    }
}
