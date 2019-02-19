using QA.Domain.Entities;
using QA.Domain.Exceptions;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Answers
{
    public class AnswerCommentTests
    {
        [Fact]
        public void WhenContributorAddsAComment_TheCommentShouldNotBeAddded_IfTheyDoNotHaveEnoughReputation()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);

            var answerContributor = new Contributor(new Name("Jane", "Doe"), new Reputation(1000));
            var answer = new Answer(question.Id, answerContributor, new PlainTextContent());

            var commentContributor = new Contributor(new Name("John", "Doe"), new Reputation(100));

            question.AddAnAnswer(answer);

            Assert.Throws<InsufficientReputationException>(() => question.CommentOnAnswer(answer.Id, new Comment(commentContributor, "This is a comment")));
        }
    }
}
