using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.ApplicationServices.Services;
using Automated.Testing.System.DataAccess.Postgres.Repositories;
using Automated.Testing.System.DataAccess.Postgres.Repositories.Interfaces;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Automated.Testing.System.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.ApplicationServices
{
    public class ApplicationServicesModule : Module
        {
            public override void Load(IServiceCollection services)
            {
                // database provider
                services.AddScoped<IPostgresService, PostgresService>();
                
                // repository
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IDictionaryRepository, DictionaryRepository>();
                
                //services
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IDictionaryService, DictionaryService>();
                
            }
    }
}