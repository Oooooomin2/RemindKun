using Microsoft.Extensions.Options;
using RemindKun.Domain.GitHub.Models.Issues.Entities;
using RemindKun.Domain.Remind;
using RemindKun.Domain.Reminder.Chatwork.ValueObjects;
using RemindKun.Infrastructure.Reminder.Settings;

namespace RemindKun.Infrastructure.Reminder
{
    /// <summary>
    /// Chatworkリマインダー
    /// </summary>
    public sealed class ChatworkReminder : IRemind
    {
        private readonly IHttpClientFactory httpClientFactory;

        private readonly ReminderSettings settings;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="httpClientFactory">HttpClientファクトリ</param>
        /// <param name="options">Reminder Settings Option</param>
        public ChatworkReminder(IHttpClientFactory httpClientFactory, IOptions<ReminderSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.settings = options.Value;
        }

        public async Task SendAsync(List<Issue> issues)
        {
            var body = $@"まだ未実施なIssueは以下です。
Url: {issues.Select(issue => issue.Url).First()}";
            var chatWork = new Domain.Reminder.Chatwork.ValueObjects.Chatwork(
                this.settings.ChatWork.EndPoint,
                this.settings.ChatWork.ApiToken,
                new Body(body));

            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, chatWork.EndPoint);

            request.Headers.Add("x-chatworktoken", chatWork.ApiToken);

            // POSTリクエストの内容
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("body", chatWork.Body.Value)
            ]);

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
    }
}
