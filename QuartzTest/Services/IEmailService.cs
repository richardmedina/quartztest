using System.Security.Cryptography.X509Certificates;

namespace QuartzTest.Services
{
    public interface IEmailService
    {
        void SendAsync(string from, string to, string subject, string content);
    }
}
