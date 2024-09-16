using Microsoft.Extensions.Options;
using RemindKun.Domain.GitHub.Models.Issues.Entities;
using RemindKun.Domain.Remind;
using RemindKun.Infrastructure.Reminder.Settings;

namespace RemindKun.Infrastructure.Reminder
{
    /// <summary>
    /// Email リマインダー
    /// </summary>
    public sealed class EmailReminder : IRemind
    {
        private readonly ReminderSettings settings;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="options">Email Settings Option</param>
        public EmailReminder(IOptions<ReminderSettings> options)
        {
            this.settings = options.Value;
        }

        public async Task SendAsync(List<Issue> issues)
        {
            var email = new Domain.Remind.Email.ValueObjects.Email(
                host: this.settings.Email.Host,
                userName: this.settings.Email.UserName,
                password: this.settings.Email.Password,
                from: "owner@gmail.com",
                to: "syu1dfgh@gmail.com",
                cc: "",
                bcc: "",
                subject: "テストメールです。",
                body: $@"まだ未実施なIssueは以下です。
Url: {issues.Select(issue => issue.Url).First()}");

            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("オーナー", email.From));
            message.To.Add(new MimeKit.MailboxAddress("みね", email.To));
            message.Subject = email.Subject;

            // 本文を作る
            var textPart = new MimeKit.TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = email.Body
            };

            // MimeMessageを完成させる
            message.Body = textPart;

            // SMTPサーバに接続してメールを送信する
            using var client = new MailKit.Net.Smtp.SmtpClient();
#if DEBUG
            // 開発用のSMTPサーバが暗号化に対応していないときは、次の行を追加する
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
#endif

            try
            {
                await client.ConnectAsync(email.Host, 587);
                await client.AuthenticateAsync(email.UserName, email.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
