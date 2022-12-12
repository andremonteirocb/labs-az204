using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Capture all log-level entries from Program
// app.AddFilter<ApplicationInsightsLoggerProvider>(
//     typeof(Program).FullName, LogLevel.Trace);
    
// // Capture all log-level entries from Startup
// app.AddFilter<ApplicationInsightsLoggerProvider>(
//     typeof(Startup).FullName, LogLevel.Trace);

// builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(
//     typeof(Program).FullName, LogLevel.Trace);

// Add services to the container.
// Para versão < 2.15.0 do pacote Microsoft.ApplicationInsights.AspNetCore
// Para versão >= 2.15.0 usar configuração no appsettings na seção ApplicationInsights
// builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
// {
//     EnableAdaptiveSampling = false,
//     EnableQuickPulseMetricStream = false
// });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
