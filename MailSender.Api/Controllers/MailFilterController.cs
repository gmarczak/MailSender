namespace MailSender.Api.Controllers
{
    using MailSender.Core.Models;
    using MailSender.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("client-app")]
    public class MailFilterController : ControllerBase
    {
        private readonly IMailFilterService _mailFilterService;
        private readonly IConfiguration _config;

        public MailFilterController(IMailFilterService mailFilterService, IConfiguration config)
        {
            _mailFilterService = mailFilterService;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationRequest request)
        {
            var result = _mailFilterService.Register(request.AppId, request.AppName, request.Pass);
            if (result)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.AppId),
                    new Claim("AppId", request.AppId),
                    new Claim("AppName", request.AppName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var secretKey = _config["JwtSettings:SecretKey"];
                var keyBytes = Encoding.UTF8.GetBytes(secretKey!);
                var key = new SymmetricSecurityKey(keyBytes);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "MailSender.Api",
                    audience: "MailSender.Clients",
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(90),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    appId = request.AppId,
                    appName = request.AppName,
                    key = new JwtSecurityTokenHandler().WriteToken(token),
                });
            }
            else
            {
                return StatusCode(403, new { error = "Invalid index-based password 53" });
            }
        }

        [Authorize]
        [HttpPost("/mail/send")]
        public IActionResult SendEmail([FromBody] InboundEmails email)
        {
            var processedEmail = _mailFilterService.ProcessEmail(email);

            var appIdFromToken = User.FindFirst("AppId")?.Value ?? "NieznaneId";
            var appNameFromToken = User.FindFirst("AppName")?.Value ?? "NieznanaNazwa";

            return Ok(new
            {
                appId = "my app id",
                appName = "my app name",
                status = "queued",
                email = new
                {
                    to = processedEmail.To,
                    subject = processedEmail.Subject,
                    body = processedEmail.Body
                }
            });
        }

    }
}