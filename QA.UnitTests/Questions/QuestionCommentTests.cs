using QA.Domain.Entities;
using QA.Domain.Exceptions;
using QA.Domain.ValueObjects;
using Xunit;

namespace QA.UnitTests.Questions
{
    public class QuestionCommentTests
    {
        [Fact]
        public void WhenContributorAddsAComment_TheCommentShouldNotBeAddded_IfTheyDoNotHaveEnoughReputation()
        {
            var poster = new Poster(new Name("Joe", "Bloggs"));
            var content = new PlainTextContent();
            var question = new Question("A question", content, poster);
            
            var contributor = new Contributor(new Name("Joe", "Bloggs"), new Reputation(100));
            
            Assert.Throws<InsufficientReputationException>(() => question.AddComment(new Comment(contributor, "This is a comment")));
        }
    }
}
