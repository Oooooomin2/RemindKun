namespace RemindKun.Infrastructure.Reminder.Settings
{
    public sealed class Email
    {
        public bool Enabled { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
