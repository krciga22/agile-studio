using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Testcontainers.MySql;

namespace AgileStudioServerTest.IntegrationTests
{
    internal class DBTestContainer
    {
        private static DBTestContainer? _singletonInstance = null;

        private MySqlContainer _dbTestContainer;

        private bool _IsStarted { get; set; } = false;

        private bool _IsMigrated { get; set; } = false;

        private DBTestContainer()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());
            var configuration = builder.Build();
            var dbName = configuration.GetValue<string>("DB_NAME");
            var dbUser = configuration.GetValue<string>("DB_USER");
            var dbPass = configuration.GetValue<string>("DB_PASS");

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
                .Build();
        }

        public static DBTestContainer GetInstance()
        {
            if(_singletonInstance == null){
                _singletonInstance = new DBTestContainer();
            }

            return _singletonInstance;
        }

        public bool IsStarted()
        {
            return _IsStarted;
        }

        public bool IsMigrated()
        {
            return _IsMigrated;
        }

        public void SetIsMigrated(bool isMigrated)
        {
            _IsMigrated = isMigrated;
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
