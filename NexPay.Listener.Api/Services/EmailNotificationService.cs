using NexPay.Listener.Api.Model;

namespace NexPay.Listener.Api.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly IConfiguration _configuration;
        public EmailNotificationService(IEmailSender emailSender, ILogger<EmailNotificationService> logger, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendRegistrationEmailNotification(UserRegistrationMessagePayload request)
        {
            _logger.LogInformation($"Begin executing SendRegistrationEmailNotification() of {nameof(SendRegistrationEmailNotification)} class.");
            
            var emailHtmlBody = GetRegistrationEmailBody(request);
            var userMessage = new Message(new string[] { request.Email }, $"Welcome Mr. {request.FirstName + " " + request.LastName} to NexPay", emailHtmlBody);
            await _emailSender.SendEmail(userMessage);
            var adminEmailAddress = _configuration.GetValue<string>("AdminEmail");
            var adminMessage = new Message(new string[] { adminEmailAddress }, $"New User Registered with Email - {request.Email}", emailHtmlBody);
            await _emailSender.SendEmail(adminMessage);

            _logger.LogInformation($"Begin executing SendRegistrationEmailNotification() of {nameof(EmailNotificationService)} class.");
        }

        public async Task SendPaymentEmailNotification(SubmitContractMessagePayload request)
        {
            _logger.LogInformation($"Begin executing SendPaymentEmailNotification() of {nameof(EmailNotificationService)} class.");

            var adminEmailAddress = _configuration.GetValue<string>("AdminEmail");
            var emailHtmlBody = GetPaymentEmailBody(request);
            var userMessage = new Message(new string[] { request.UserEmail }, $"Contract Generated - {request.ContractId}", emailHtmlBody);
            await _emailSender.SendEmail(userMessage);
            var adminMessage = new Message(new string[] { adminEmailAddress }, $"Contract Generated - {request.ContractId}, waiting for your approval", emailHtmlBody);
            await _emailSender.SendEmail(adminMessage);

            _logger.LogInformation($"Begin executing SendPaymentEmailNotification() of {nameof(EmailNotificationService)} class.");
        }

        private string GetRegistrationEmailBody(UserRegistrationMessagePayload request)
        {
            var emailHtmlBody = $"<span>First Name - <b>{request.FirstName}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Last Name - <b>{request.LastName}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Email - <b>{request.Email}</b></span>";
            emailHtmlBody += $"<br/>"; 
            emailHtmlBody += $"<span>User Id - <b>{request.Id}</b></span>";
            return emailHtmlBody;
        }

        private string GetPaymentEmailBody(SubmitContractMessagePayload request)
        {
            var emailHtmlBody = $"<span>Contract Id - <b>{request.ContractId}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Currency From - <b>{request.FromCurrencyCode}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Currency To - <b>{request.ToCurrencyCode}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Amount to Convert - <b>{request.InitialAmount}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Conversion Rate - <b>{request.ConversionRate}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Final Amount - <b>{request.FinalAmount}</b></span>";
            emailHtmlBody += $"<br/>";
            emailHtmlBody += $"<span>Contract Status - <b>{request.ContractStatus}</b></span>";
            return emailHtmlBody;
        }
    }
}
