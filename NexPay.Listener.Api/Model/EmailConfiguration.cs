namespace NexPay.Listener.Api.Model
{
    public class EmailConfiguration
    {
        /// <summary>
        /// Gets or set From.
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets SmtpServer.
        /// </summary>
        public string? SmtpServer { get; set; }

        /// <summary>
        /// Gets or sets Port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string? Password { get; set; }
    }
}
