using Devon4Net.Application.WebAPI.Configuration;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Devon4Net.Infrastructure.Azure.Configuration;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Middleware.Middleware;
using Devon4Net.Infrastructure.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonFw(builder.Host);

#region services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureDevonfwAzure(builder.Configuration, true);
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