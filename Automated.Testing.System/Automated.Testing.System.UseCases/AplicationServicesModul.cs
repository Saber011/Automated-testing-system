using System;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.ApplicationServices.Services;
using Automated.Testing.System.DataAccess.Postgres;
using Automated.Testing.System.UseCases.Handlers.Dictionary.Queries.GetAllDictionary;
using Automated.Testing.System.Utils.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Automated.Testing.System.UseCases
{
    public class ApplicationServicesModule : Module
        {
            public override void Load(IServiceCollection services)
            {
                services.RegisterModule<PostgresDataAccessModule>(Configuration);
                
                //services
                services.AddScoped<IDictionaryService, DictionaryService>();
                
                services.AddMediatR(typeof(GetAllDictionaryRequest));
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            }
    }
}