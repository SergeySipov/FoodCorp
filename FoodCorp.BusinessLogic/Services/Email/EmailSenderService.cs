using FoodCorp.Configuration.Constants;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FoodCorp.BusinessLogic.Services.Email;

public class EmailSenderService : IEmailSenderService
{
    private readonly IOptions<SmtpSettings> _smtpSettings;

    public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(AppSettingConstants.ProjectName, _smtpSettings.Value.SenderMail));
        emailMessage.To.Add(new MailboxAddress(string.Empty, email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync(_smtpSettings.Value.Host, _smtpSettings.Value.Port, true);
        await client.AuthenticateAsync(_smtpSettings.Value.SenderMail, _smtpSettings.Value.SenderMailPassword);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
    }
}
