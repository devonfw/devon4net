using Devon4Net.Application.Kafka.Business.KafkaManagement.Services;
using Devon4Net.Application.Kafka.Business.Messages;
using Devon4Net.Infrastructure.Cors;
using Devon4Net.Infrastructure.Kafka;
using Devon4Net.Infrastructure.Swagger;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;
using Devon4Net.Infrastructure.Common.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.InitializeDevonfwApi(builder.Host);

#region services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var devonfwOptions = builder.Services.SetupDevonfw(builder.Configuration);
builder.Services.SetupMiddleware(builder.Configuration);
builder.Services.SetupLog(builder.Configuration);
builder.Services.SetupSwagger(builder.Configuration);

//KAFKA CONFIGURATION
builder.Services.SetupKafka(builder.Configuration);
builder.Services.AddKafkaStreamService<FileTransferStreamService, string, DataPieceDto<byte[]>>("file_transfer");
builder.Services.AddKafkaStreamService<MessageStreamService, string, string>("message_stream");
#endregion

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

app.UseAuthorization();
app.MapControllers();

app.Run();

