using Automated.Testing.System.Analytics.DataAccess.Postgres.Repositories;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Automated.Testing.System.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.Analytics.DataAccess.Postgres
{
    public class DataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IPostgresService, PostgresService>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();
        }
    }
}
