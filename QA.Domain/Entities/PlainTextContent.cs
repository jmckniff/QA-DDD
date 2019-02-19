using QA.Domain.Enums;

namespace QA.Domain.Entities
{
    public class PlainTextContent : BodyContent
    {
        public PlainTextContent() : base(BodyContentType.PlainText, "")
        {

        }

        public PlainTextContent(string content) : base(BodyContentType.PlainText, content)
        {
            
        }
    }
}