using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RemindKun.Application.GitHub;
using RemindKun.Domain.GitHub;
using RemindKun.Domain.Remind;
using RemindKun.Infrastructure.Api;
using RemindKun.Infrastructure.Api.Settings;
using RemindKun.Infrastructure.Reminder;
using RemindKun.Infrastructure.Reminder.Settings;

namespace RemindKun
{
    public sealed class Program
    {
        private static ServiceProvider serviceProvider;

        static async Task Main(string[] args)
        {
            StartUp();

            var gitHubApplicationService = serviceProvider.GetRequiredService<GitHubApplicationService>();
            await gitHubApplicationService.RemindAsync();
        }

        private static void StartUp()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure<ReminderSettings>(configuration.GetSection(nameof(ReminderSettings)));
            serviceCollection.Configure<GitHubSettings>(configuration.GetSection(nameof(GitHubSettings)));
            serviceCollection.AddHttpClient();
            
            // GitHub関連
            serviceCollection.AddTransient<IGitHubApiClient, GitHubApiClient>();
            serviceCollection.AddTransient<GitHubApplicationService>();

            // 通知関連
            var settings = configuration.GetSection(nameof(ReminderSettings)).Get<ReminderSettings>()!;
            if (settings.Email.Enabled)
            {
                serviceCollection.AddTransient<IRemind, EmailReminder>();
            }
            
            if(settings.ChatWork.Enabled)
            {
                serviceCollection.AddTransient<IRemind, ChatworkReminder>();
            }

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
