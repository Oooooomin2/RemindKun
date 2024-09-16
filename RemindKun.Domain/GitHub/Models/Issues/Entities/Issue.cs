using RemindKun.Domain.GitHub.Models.Issues.ValueObjects;

namespace RemindKun.Domain.GitHub.Models.Issues.Entities
{
    public sealed class Issue
    {
        public Id Id { get; }

        public string Url { get; }

        public string UserName { get; }

        public List<long> LabelIds { get; }

        public string Body { get; }

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; }

        public Issue(
            Id id,
            string url,
            string userName,
            List<long> labelIds,
            string body,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            LabelIds = labelIds ?? throw new ArgumentNullException(nameof(labelIds));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
