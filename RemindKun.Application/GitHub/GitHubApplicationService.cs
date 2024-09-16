using RemindKun.Domain.GitHub;
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
            var openIssuesEntity = await gitHubApiClient.GetOpenIssuesAsync();

            foreach(var remind in reminds)
            {
                await remind.SendAsync(openIssuesEntity);
            }
        }
    }
}
