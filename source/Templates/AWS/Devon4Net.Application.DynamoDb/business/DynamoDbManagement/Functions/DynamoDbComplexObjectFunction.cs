using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Dto;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Functions
{
    internal class DynamoDbComplexObjectFunction : LambdaFunction<string, ObjectTest>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDynamoDbTableRepository, DynamoDbTableRepository>();
            services.AddTransient<ILambdaEventHandler<string, ObjectTest>, ComplexObjectFunctionEventHandler>();
        }
    }
}