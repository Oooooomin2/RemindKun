namespace RemindKun.Domain.GitHub.Models.Issues.ValueObjects
{
    public sealed class Id
    {
        public Id(long id)
        {
            Value = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Id id &&
                   Value == id.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
        
        public long Value { get; }
    }
}
