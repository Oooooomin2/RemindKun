﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RemindKun.Domain.GitHub;
using RemindKun.Domain.GitHub.Models.Issues.Entities;
using RemindKun.Domain.GitHub.Models.Issues.ValueObjects;
using RemindKun.Infrastructure.Api.Settings;
using System.Net.Http.Headers;

namespace RemindKun.Infrastructure.Api
{
    /// <summary>
    /// GitHub APIクライアント
    /// </summary>
    public sealed class GitHubApiClient : IGitHubApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly GitHubSettings settings;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="httpClientFactory">HttpClientファクトリ</param>
        /// <param name="options">GitHub Settings Option</param>
        public GitHubApiClient(IHttpClientFactory httpClientFactory, IOptions<GitHubSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            settings = options.Value;
        }

        public async Task<List<Issue>> GetOpenIssuesAsync()
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, this.settings.EndPoint);

            request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
            request.Headers.Add("User-Agent", "RemindKun");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.settings.Token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Httpリクエストでエラーが発生しました。");
            }

            var content = await response.Content.ReadAsStringAsync();
            var openIssues = JsonConvert.DeserializeObject<List<Response.Issue>>(content) ?? [];

            return openIssues.Select(issue => new Issue(
                    id: new Id(issue.Id),
                    url: issue.Url,
                    userName: issue.User.Login,
                    labelIds: issue.Labels.Select(label => label.Id).ToList(),
                    body: issue.Body,
                    createdAt: issue.CreatedAt,
                    updatedAt: issue.UpdatedAt))
                .ToList();
        }
    }
}
