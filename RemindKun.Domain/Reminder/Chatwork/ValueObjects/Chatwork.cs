namespace RemindKun.Domain.Reminder.Chatwork.ValueObjects
{
    public sealed class Chatwork
    {
        public string EndPoint { get; }
        public string ApiToken { get; }
        public Body Body { get; }

        public Chatwork(string endPoint, string apiToken, Body body)
        {
            EndPoint = endPoint;
            ApiToken = apiToken;
            Body = body;
        }
    }
}
