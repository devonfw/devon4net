using Devon4Net.Infrastructure.Grpc;
using Devon4Net.Infrastructure.Swagger;
using Devon4Net.Infrastructure.Cors;
using Devon4Net.Application.GrpcService;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;
using Devon4Net.Infrastructure.Common.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonfwApi(builder.Host);
builder.Services.AddControllers();

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
app.MapGrpcService<GreeterService>();
app.SetupCors();
app.SetupMiddleware(builder.Services);
app.ConfigureSwaggerEndPoint();
if (devonfwOptions.ForceUseHttpsRedirection || (!devonfwOptions.UseIIS && devonfwOptions.Kestrel.UseHttps))
{
    app.UseHttpsRedirection();
}
#endregion

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();