using Microsoft.AspNetCore.Mvc;
using NexPay.Listener.Api.Model;
using NexPay.Listener.Api.Services;

namespace NexPay.Listener.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailNotificationService _emailNotificationService;
        public EmailController(ILogger<EmailController> logger, IEmailNotificationService emailNotificationService)
        {
            _logger = logger;
            _emailNotificationService = emailNotificationService;   
        }

        [HttpPost("sendPaymentEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void SendPaymentEmail(SubmitContractMessagePayload payload)
        {
            _logger.LogInformation($"Begin executing SendPaymentEmail() of {nameof(EmailController)} class.");

            await _emailNotificationService.SendPaymentEmailNotification(payload);

            _logger.LogInformation($"Finish executing SendPaymentEmail() of {nameof(EmailController)} class.");
        }

        [HttpPost("sendRegistrationEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void SendRegistrationEmail(UserRegistrationMessagePayload payload)
        {
            _logger.LogInformation($"Begin executing SendRegistrationEmail() of {nameof(EmailController)} class.");

            await _emailNotificationService.SendRegistrationEmailNotification(payload);

            _logger.LogInformation($"Finish executing SendRegistrationEmail() of {nameof(EmailController)} class.");
        }
    }
}
