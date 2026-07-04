namespace MailSender.Core.Models
{
    public class RegistrationRequest
    {
        public required string AppId { get; set; }
        public required string AppName { get; set; }
        public required string Pass { get; set; }

    }
}