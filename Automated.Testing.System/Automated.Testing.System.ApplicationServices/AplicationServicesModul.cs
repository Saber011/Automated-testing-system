using System;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.ApplicationServices.Services;
using Automated.Testing.System.DataAccess.Postgres;
using Automated.Testing.System.Utils.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.ApplicationServices
{
    public class ApplicationServicesModule : Module
        {
            public override void Load(IServiceCollection services)
            {
                services.RegisterModule<PostgresDataAccessModule>(Configuration);
                
                //services
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IDictionaryService, DictionaryService>();
                services.AddScoped<IAccountService, AccountService>();
                
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            }
    }
}