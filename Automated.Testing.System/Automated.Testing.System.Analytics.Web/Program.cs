using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Automated.Testing.System.Utils.Modules;
using Automated.Testing.System.Analytics.Infrastructure.Implementation;
using Automated.Testing.System.Analytics.DataAccess.Postgres;
using Automated.Testing.System.Analytics.UseCases;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Mappings;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Analytics.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(StatisticAutoMapperProfile));

builder.Services.AddOptions();
builder.Services.Configure<PostgresConfig>(builder.Configuration.GetSection("Postgres"));
builder.Services.RegisterModule<DataAccessModule>(builder.Configuration);
builder.Services.RegisterModule<InfrastructureModule>(builder.Configuration);
builder.Services.RegisterModule<UseCasesModule>(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();