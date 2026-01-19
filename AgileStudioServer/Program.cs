using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using IPNetwork = Microsoft.AspNetCore.HttpOverrides.IPNetwork;

namespace AgileStudioServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // todo update DBContextFactory to get configuration via dependency injection
            builder.Services.AddMyDB(builder.Configuration);
            builder.Services.AddMyRepositories();
            builder.Services.AddMyCoreServices();
            builder.Services.AddControllers();
            builder.Services.AddMyCoreFeatureServices();
            builder.Services.AddMyDtoHydrators();
            builder.Services.AddMyModelHydrators();
            builder.Services.AddMyEntityHydrators();
            builder.Services.AddMyAuth(builder.Configuration);
            builder.Services.AddMyCors(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            bool webProxyEnabled = builder.Configuration.GetValue<bool>("WEB_PROXY_ENABLED");
            if (webProxyEnabled)
            {
                var forwardedHeaderOptions = CreateForwardedHeadersOptions(builder.Configuration);
                app.UseForwardedHeaders(forwardedHeaderOptions);
            }
            else
            {
                app.UseHttpsRedirection();
            }
                
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(Constants.CorsPolicyDefault);
            app.MapControllers();
            app.Run();
        }

        /// <summary>
        /// Create forwarded headers for use when behind 
        /// a web proxy
        /// </summary>
        private static ForwardedHeadersOptions CreateForwardedHeadersOptions(IConfiguration configuration)
        {
            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                        ForwardedHeaders.XForwardedProto
            };

            IList<IPNetwork> knownNetworkIPs = GetWebProxyKnownNetworkIPs(configuration);
            foreach (var ipNetwork in knownNetworkIPs){
                forwardedHeaderOptions.KnownNetworks.Add(ipNetwork);
            }

            return forwardedHeaderOptions;
        }

        /// <summary>
        /// Get web proxy known network IPs from configuration
        /// </summary>
        private static IList<IPNetwork> GetWebProxyKnownNetworkIPs(IConfiguration configuration)
        {
            string[]? knownNetworkIPs = configuration.GetSection("WEB_PROXY_KNOWN_NETWORKS").Get<string[]?>();
            if (knownNetworkIPs == null || knownNetworkIPs.Length == 0)
            {
                throw new Exception("WEB_PROXY_ENABLED is true but no WEB_PROXY_KNOWN_NETWORKS are configured");
            }

            IList<IPNetwork> ipNetworks = new List<IPNetwork>();
            foreach (string networkIp in knownNetworkIPs)
            {
                string[] parts = networkIp.Split('/');
                if (parts.Length == 2 && IPAddress.TryParse(parts[0], out IPAddress? ip) && Int32.TryParse(parts[1], out int cidr))
                {
                    ipNetworks.Add(new IPNetwork(ip, cidr));
                }
            }

            return ipNetworks;
        }
    }
}