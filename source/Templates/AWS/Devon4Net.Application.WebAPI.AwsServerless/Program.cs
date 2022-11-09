using Devon4Net.Infrastructure.AWS.Serverless;
using Devon4Net.Infrastructure.Cors;
using Devon4Net.Infrastructure.Swagger;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;
using Devon4Net.Infrastructure.Common.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonfwApi(builder.Host);

#region services
builder.Services.SetupDevonfwAws(builder.Configuration, true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupDevonfwAws(builder.Configuration, true);
builder.Services.SetupMiddleware(builder.Configuration);
builder.Services.SetupLog(builder.Configuration);
builder.Services.SetupSwagger(builder.Configuration);
#endregion

var app = builder.Build();

#region devon app
app.ConfigureSwaggerEndPoint();
app.SetupMiddleware(builder.Services);
app.SetupCors();
#endregion

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();