using System;
using System.Text;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.ApplicationServices.Services;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Automated.Testing.System.DataAccess.Postgres.Repositories;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Automated.Testing.System.Web.SwaggerConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Automated.Testing.System.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<PostgresConfig>(Configuration.GetSection("Postgres"));
            var isTestEnv = !string.IsNullOrEmpty(Configuration["TestEnvironment"]);
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AuthenticationSettingsConfig>(appSettingsSection);
            services.AddSwagger(isTestEnv);
            var appSettings = appSettingsSection.Get<AuthenticationSettingsConfig>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // core
            services.AddScoped<IPostgresService, PostgresService>();
            
            // repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDictionaryRepository, DictionaryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            
            // service
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<IAccountService, AccountService>();
                
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<ExceptionHandlingMiddleware>();
            
            services.AddHttpContextAccessor();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseProjectSwagger();

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}