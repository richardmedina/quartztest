namespace QuartzTest.Services
{
    public class EmailService : IEmailService
    {
        private ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public void SendAsync(string from, string to, string subject, string content)
        {
            _logger.LogInformation($"sending email from: {from}, to: {to}, subject: {subject}, content: {content}");
        }
    }
}
