using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Testcontainers.MySql;

namespace AgileStudioServerTest.IntegrationTests
{
    public class DBTestContainer
    {
        private static DBTestContainer? _singletonInstance = null;

        private MySqlContainer _dbTestContainer;

        private bool _IsStarted { get; set; } = false;

        private DBTestContainer(IConfiguration? configuration = null)
        {
            string dbName, dbUser, dbPass;

            if(configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .AddEnvironmentVariables();

                configuration = builder.Build();
            }

            dbName = configuration.GetValue<string>("DB_NAME") ?? "";
            if(String.IsNullOrEmpty(dbName)){
                throw new Exception("DB_NAME not found in configuration");
            }

            dbUser = configuration.GetValue<string>("DB_USER") ?? "";
            if (String.IsNullOrEmpty(dbUser)){
                throw new Exception("DB_USER not found in configuration");
            }

            dbPass = configuration.GetValue<string>("DB_PASS") ?? "";
            if (String.IsNullOrEmpty(dbPass)){
                throw new Exception("DB_PASS not found in configuration");
            }

            _dbTestContainer = new MySqlBuilder()
                .WithImage("mysql:8.0.42")
                .WithLabel("reuse-id", "8d58e958-abd1-438b-a481-90ee0ccbfc09")
                .WithDatabase(dbName)
                .WithUsername(dbUser)
                .WithPassword(dbPass)
                .WithPortBinding(3306, false)
                .WithExposedPort(3306)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
                .WithReuse(true)
                .WithEnvironment("MYSQL_RANDOM_ROOT_PASSWORD", "yes")
                .WithEnvironment("MYSQL_DATABASE", dbName)
                .WithEnvironment("MYSQL_USER", dbUser)
                .WithEnvironment("MYSQL_PASSWORD", dbPass)
                .Build();
        }

        public static DBTestContainer GetInstance(IConfiguration? configuration = null)
        {
            if(_singletonInstance == null){
                _singletonInstance = new DBTestContainer(configuration);
            }

            return _singletonInstance;
        }

        public bool IsStarted()
        {
            return _IsStarted;
        }

        public void Start()
        {
            if (!_IsStarted){
                _IsStarted = true;
                _dbTestContainer.StartAsync().GetAwaiter().GetResult();
            }
        }

        public void Stop()
        {
            if (_IsStarted){
                _dbTestContainer.StopAsync().GetAwaiter().GetResult();
                _IsStarted = false;
            }
        }

        public string GetConnectionString()
        {
            return _dbTestContainer.GetConnectionString();
        }
    }
}
