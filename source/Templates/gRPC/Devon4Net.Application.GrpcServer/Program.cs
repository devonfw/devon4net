using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Devon4Net.Infrastructure.Common.Options.Devon;
using Devon4Net.Infrastructure.Grpc;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Middleware.Middleware;
using Devon4Net.Infrastructure.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonFw(builder.Host);

#region services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var devonfwOptions = builder.Services.SetupDevonfw(builder.Configuration);
builder.Services.SetupMiddleware(builder.Configuration);
builder.Services.SetupLog(builder.Configuration);
builder.Services.SetupSwagger(builder.Configuration);
builder.Services.AddGrpc();
#endregion

var app = builder.Build();

#region devon app

app.ConfigureSwaggerEndPoint();
app.SetupMiddleware(builder.Services);
app.SetupCors();
app.SetupGrpcServices(new List<string> { "Devon4Net.Application.GrpcServer" });
if (devonfwOptions.ForceUseHttpsRedirection || (!devonfwOptions.UseIIS && devonfwOptions.Kestrel.UseHttps))
{
    app.UseHttpsRedirection();
}
#endregion

app.UseHttpsRedirection();
app.MapControllers();

app.Run();