namespace RemindKun.Domain.Remind.Email.ValueObjects
{
    public sealed class Email
    {
        public string Host { get; }

        public string UserName { get; }

        public string Password { get; }

        public string From { get; }

        public string To { get; }

        public string Cc { get; }

        public string Bcc { get; }

        public string Subject { get; }

        public string Body { get; }

        public Email(
            string host,
            string userName,
            string password,
            string from,
            string to,
            string cc,
            string bcc,
            string subject,
            string body)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
        }
    }
}
