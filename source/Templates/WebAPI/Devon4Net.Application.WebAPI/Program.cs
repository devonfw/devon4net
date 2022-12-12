using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.Domain.UnitOfWork;
using Devon4Net.Infrastructure.CircuitBreaker;
using Devon4Net.Infrastructure.Cors;
using Devon4Net.Infrastructure.Grpc;
using Devon4Net.Infrastructure.Kafka;
using Devon4Net.Infrastructure.Swagger;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;
using Devon4Net.Infrastructure.Common.Application.Middleware;
using Devon4Net.Infrastructure.JWT;
using Devon4Net.Infrastructure.UnitOfWork;
using Devon4Net.Infrastructure.LiteDb;
using Devon4Net.Infrastructure.RabbitMQ;
using Devon4Net.Infrastructure.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonfwApi(builder.Host);
builder.Services.AddControllers();

#region devon services
var devonfwOptions = builder.Services.SetupDevonfw(builder.Configuration);
builder.Services.SetupLog(builder.Configuration!);
builder.Services.SetupMiddleware(builder.Configuration);
builder.Services.SetupSwagger(builder.Configuration);
builder.Services.SetupCircuitBreaker(builder.Configuration);
builder.Services.SetupCors(builder.Configuration);
builder.Services.SetupJwt(builder.Configuration);
builder.Services.SetupUnitOfWork(typeof(Program));
builder.Services.SetupLiteDb(builder.Configuration);
builder.Services.SetupRabbitMq(builder.Configuration);
builder.Services.SetupMediatR(builder.Configuration);
builder.Services.SetupKafka(builder.Configuration);
builder.Services.SetupGrpc(builder.Configuration);
#endregion

builder.Services.SetupCustomDependencyInjection(builder.Configuration);

var app = builder.Build();

#region devon app
app.ConfigureSwaggerEndPoint();
app.SetupMiddleware(builder.Services);
app.SetupCors();

if (devonfwOptions.ForceUseHttpsRedirection || (!devonfwOptions.UseIIS && devonfwOptions.Kestrel.UseHttps))
{
    app.UseHttpsRedirection();
}
#endregion

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
