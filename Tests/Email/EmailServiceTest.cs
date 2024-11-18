using Moq;
using Services.Email;
using Microsoft.Extensions.Configuration;

namespace Tests.Email;

public class EmailServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        // Configuração simulada para a chave API do SendGrid
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["ExternalServices:SendGridApiKey"])
            .Returns("fake-api-key"); // Use uma chave de API falsa para o teste

        // Inicializa o serviço de e-mail com a configuração simulada
        _emailService = new EmailService(_configurationMock.Object);
    }

    [Fact]
    public async Task SendEmailAsync_InvalidApiKey_ThrowsArgumentNullException()
    {
        // Arrange
        _configurationMock.Setup(c => c["ExternalServices:SendGridApiKey"])
            .Returns(""); // Configura uma chave API inválida para teste

        var emailServiceWithInvalidApiKey = new EmailService(_configurationMock.Object);

        var to = "test@example.com";
        var subject = "Test Subject";
        var body = "This is a test email body.";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => emailServiceWithInvalidApiKey.SendEmailAsync(to, subject, body));
    }
}