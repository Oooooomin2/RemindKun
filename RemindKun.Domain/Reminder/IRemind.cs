using RemindKun.Domain.GitHub.Models.Issues.Entities;

namespace RemindKun.Domain.Remind
{
    /// <summary>
    /// リマインダーインターフェース
    /// </summary>
    public interface IRemind
    {
        /// <summary>
        /// 対象のIssueをリマインドする。
        /// </summary>
        /// <param name="issues">リマインド対象のIssue</param>
        Task SendAsync(List<Issue> issues);
    }
}
