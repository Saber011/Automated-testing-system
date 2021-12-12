using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Automated.Testing.System.DataAccess.Interfaces;
using Automated.Testing.System.DataAccess.Postgres.Repositories;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Automated.Testing.System.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.DataAccess.Postgres
{
    public class PostgresDataAccessModule : Module
        {
            public override void Load(IServiceCollection services)
            {
                services.AddScoped<IPostgresService, PostgresService>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IDictionaryRepository, DictionaryRepository>();
            }
    }
}