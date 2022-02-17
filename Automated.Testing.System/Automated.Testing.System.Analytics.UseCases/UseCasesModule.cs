using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Mappings;
using Automated.Testing.System.Utils.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.Analytics.UseCases
{
    public class UseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(StatisticAutoMapperProfile));
        }
    }
}
