namespace Backend.Communication.RabbitMqConfuguration
{ 
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string VHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
        public int RetryWait { get; set; }
        public string ActionBenefitQueueName { get; set; }
        public string TenderExchangeName { get; set; }
    }
}
