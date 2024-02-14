using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;
using Devon4Net.Infrastructure.Common.Application.Middleware;
using Devon4Net.Infrastructure.Cors;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.MediatR;
using Devon4Net.Infrastructure.UnitOfWork;
using Devon4Net.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.WebHost.InitializeDevonfwApi(builder.Host);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.SetupDevonfw(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupLog(builder.Configuration);
builder.Services.SetupMiddleware(builder.Configuration);
builder.Services.SetupUnitOfWork();
builder.Services.SetupMediatR(builder.Configuration);
builder.Services.SetupCustomDependencyInjection(builder.Configuration);
builder.Services.SetupCors(builder.Configuration);

var app = builder.Build();
app.SetupCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SetupMiddleware(builder.Services);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();