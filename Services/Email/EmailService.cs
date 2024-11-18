using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var apiKey = _configuration["ExternalServices:SendGridApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("iago.carapia.uttemberg@gmail.com", "Iago Uttemberg");
        var toEmail = new EmailAddress(to);
        var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, body, body);
        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            // Log or handle error if needed
            throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
        }
    }
}