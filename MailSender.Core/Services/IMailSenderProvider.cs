namespace MailSender.Core.Services;

public interface IMailSenderProvider
{
    Task SendAsync(string to, string subject, string body);
}