using System.Linq;
using System.Text;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Test.Interfaces;
using Automated.Testing.System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Automated.Testing.System.Test
{
    /// <inheritdoc cref='IApplicationFactory'/>
    public sealed class ApplicationFactory : WebApplicationFactory<Startup>, IApplicationFactory
    {
        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Test")
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    var authServices  = services.Where(x => x.ServiceType.FullName.Contains("Microsoft.AspNetCore.Authentication.AuthenticationOptions")).ToArray();
                    services.Remove(authServices[2]);
                    services.Remove(authServices[3]);
                });
        }

        /// <inheritdoc />
        public ITestEnvironment CreateEnvironment(string controllerPath)
        {
            Guard.NotNullOrWhiteSpace(controllerPath, nameof(controllerPath));

            var path = new StringBuilder()
                .Append(controllerPath.TrimEnd('/'))
                .Append('/')
                .ToString();

            return new TestEnvironment(this, path);
        }
    }
}