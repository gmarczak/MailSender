namespace MailSender.Core.Services
{
    using MailSender.Core.Models;
    public interface IMailFilterService
    {
        bool Register(string AppId, string AppName, string Pass);
        InboundEmails ProcessEmail(InboundEmails email);
    }

    public class MailFilterService : IMailFilterService
    {
        private readonly string _expectedPassword;
        public MailFilterService(string expectedPassword)
        {
            _expectedPassword = expectedPassword;
        }

        public bool Register(string AppId, string AppName, string Pass)
        {
            if (Pass == _expectedPassword)
            {
                return true;
            }
            return false;
        }
        public InboundEmails ProcessEmail(InboundEmails email)
        {
            if (!string.IsNullOrEmpty(email.Subject) && email.Subject.EndsWith("?"))
            {
                email.Subject = $"[Q] {email.Subject}";
            }
            if (!string.IsNullOrEmpty(email.Body))
            {
                email.Body = email.Body.Replace("Marczak", "[student.surname]Marczak[/student.surname]");
                email.Body = email.Body.Replace("Koń", "[student.surname]Koń[/student.surname]");
                email.Body = email.Body.Replace("Francuz", "[student.surname]Francuz[/student.surname]");
            }
            return email;
        }
    }
}