using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MailSender.Core.Services;
using Microsoft.Extensions.Configuration;

namespace MailSender.Infrastructure;


public class BrevoMailSenderProvider : IMailSenderProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public BrevoMailSenderProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var apiKey = _configuration["Brevo:ApiKey"];
        var senderEmail = _configuration["Brevo:SenderEmail"];
        var senderName = _configuration["Brevo:SenderName"] ?? "MailSender";

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("Missing Brevo:ApiKey");

        if (string.IsNullOrWhiteSpace(senderEmail))
            throw new InvalidOperationException("Missing Brevo:SenderEmail");

        var payload = new
        {
            sender = new
            {
                name = senderName,
                email = senderEmail
            },
            to = new[]
            {
                new { email = to }
            },
            subject = subject,
            textContent = body
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.brevo.com/v3/smtp/email");
        request.Headers.Add("api-key", apiKey);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Brevo error: {response.StatusCode}, {error}");
        }
    }
}