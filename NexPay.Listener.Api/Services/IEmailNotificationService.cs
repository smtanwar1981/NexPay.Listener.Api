using NexPay.Listener.Api.Model;

namespace NexPay.Listener.Api.Services
{
    public interface IEmailNotificationService
    {
        /// <summary>
        /// Send payment email notification to user and admin.
        /// </summary>
        /// <param name="request">Message payload.</param>
        /// <returns>An empty task.</returns>
        Task SendPaymentEmailNotification(SubmitContractMessagePayload request);

        /// <summary>
        /// Send registration email notification to user and admin.
        /// </summary>
        /// <param name="request">Message payload.</param>
        /// <returns>An empty task.</returns>
        Task SendRegistrationEmailNotification(UserRegistrationMessagePayload request);
    }
}
