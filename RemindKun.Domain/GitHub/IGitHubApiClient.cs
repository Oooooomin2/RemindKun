using RemindKun.Domain.GitHub.Models.Issues.Entities;

namespace RemindKun.Domain.GitHub
{
    public interface IGitHubApiClient
    {
        /// <summary>
        /// OpenしているIssue一覧を取得する。
        /// </summary>
        /// <returns>OpenしているIssue一覧</returns>
        Task<List<Issue>> GetOpenIssuesAsync();
    }
}
