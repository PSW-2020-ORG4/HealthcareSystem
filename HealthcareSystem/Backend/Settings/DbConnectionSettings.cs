namespace Backend.Settings
{
    public class DbConnectionSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public int RetryCount { get; set; }
        public int RetryWaitInSeconds { get; set; }

        public string ConnectionString
        {
            get => $"server={Host} ;userid={User}; pwd={Password};"
                   + $"port={Port}; database={Database}";
        }
    }
}
