namespace QA.Domain.ValueObjects
{
    public class Name
    {
        public string FirstName { get; }
        public string MiddleNames { get; }
        public string LastName { get; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Name(string firstName, string middleNames, string lastName)
        {
            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
        }
    }
}