using System;

namespace EventSourcingService.Settings
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

        public DbConnectionSettings()
        {

        }

        public DbConnectionSettings(string herokuPostgresURL, int retryCount, int retryWaitInSeconds)
        {
            var databaseUri = new Uri(herokuPostgresURL);
            var userInfo = databaseUri.UserInfo.Split(':');
            Host = databaseUri.Host;
            Port = databaseUri.Port.ToString();
            User = userInfo[0];
            Password = userInfo[1];
            Database = databaseUri.LocalPath.TrimStart('/');
            RetryCount = retryCount;
            RetryWaitInSeconds = retryWaitInSeconds;
        }
    }
}
