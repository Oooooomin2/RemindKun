namespace RemindKun.Domain.Reminder.Chatwork.ValueObjects
{
    public sealed class Body
    {
        private readonly int limitUpperLength = 65535;
        public string Value { get; }

        public Body(string body)
        {
            if (string.IsNullOrEmpty(body)) throw new ArgumentNullException(nameof(body));
            if (body.Length > this.limitUpperLength) throw new ArgumentException(null, nameof(body));

            Value = body;
        }
    }
}
