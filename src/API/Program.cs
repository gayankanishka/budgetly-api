using Budgetly.API.Filters;
using Budgetly.API.Services;
using Budgetly.Application;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure;
using Budgetly.Infrastructure.Logger;
using Serilog;
using LoggerConfigurationExtensions = Budgetly.Infrastructure.Logger.LoggerConfigurationExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string APPLICATION_NAME = "Budgetly-API";
LoggerConfigurationExtensions.SetupLoggerConfiguration(APPLICATION_NAME);

builder.Services.AddApplicationInsightsTelemetry();

builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
{
    loggerConfiguration.ConfigureBaseLogging(APPLICATION_NAME);
    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
});

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddCors(o =>
        o.AddDefaultPolicy(b =>
            b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();