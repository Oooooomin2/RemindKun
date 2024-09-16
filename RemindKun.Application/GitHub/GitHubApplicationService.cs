using RemindKun.Domain.GitHub.Api.Interfaces;
using RemindKun.Domain.GitHub.Models.Issues.Entities;
using RemindKun.Domain.GitHub.Models.Issues.ValueObjects;
using RemindKun.Domain.Remind;

namespace RemindKun.Application.GitHub
{
    /// <summary>
    /// GitHubアプリケーションサービス
    /// </summary>
    public sealed class GitHubApplicationService
    {
        private readonly IGitHubApiClient gitHubApiClient;

        private readonly IEnumerable<IRemind> reminds;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gitHubApiClient">GitHubAPIクライアント</param>
        /// <param name="reminds">リマインダーリスト</param>
        public GitHubApplicationService(
            IGitHubApiClient gitHubApiClient,
            IEnumerable<IRemind> reminds)
        {
            this.gitHubApiClient = gitHubApiClient;
            this.reminds = reminds;
        }

        public async Task RemindAsync()
        {
            var openIssues = await gitHubApiClient.GetOpenIssuesAsync();
            var issuesEntity = openIssues
                .Select(issue => new Issue(
                    id: new Id(issue.Id),
                    url: issue.Url,
                    userName: issue.User.Login,
                    labelIds: issue.Labels.Select(label => label.Id).ToList(),
                    body: issue.Body,
                    createdAt: issue.CreatedAt,
                    updatedAt: issue.UpdatedAt))
                .ToList();

            foreach(var remind in reminds)
            {
                await remind.SendAsync(issuesEntity);
            }
        }
    }
}
