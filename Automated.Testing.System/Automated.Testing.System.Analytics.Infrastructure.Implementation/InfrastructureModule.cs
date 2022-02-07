using Automated.Testing.System.Analytics.Infrastructure.Implementation.Services;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.Services;
using Automated.Testing.System.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.Analytics.Infrastructure.Implementation
{
    public class InfrastructureModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IExportService, ExportService>();
        }
    }
}
