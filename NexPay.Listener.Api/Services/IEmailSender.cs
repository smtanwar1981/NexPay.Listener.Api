using NexPay.Listener.Api.Model;

namespace NexPay.Listener.Api.Services
{
    public interface IEmailSender
    {
        Task SendEmail(Message message);
    }
}
