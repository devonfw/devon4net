using Amazon.Lambda.Core;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Domain.Tables;
using Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Handlers;
using Devon4Net.Infrastructure.AWS.DynamoDb.Domain.Repository;
using Devon4Net.Infrastructure.AWS.Lambda;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Devon4Net.Application.DynamoDb.business.DynamoDbManagement.Functions
{
    internal class DynamoDbManagementFunction : LambdaFunction<string, DynamoTable>
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDynamoDbRepository<DynamoTable>, DynamoDbRepository<DynamoTable>>();
            services.AddTransient<ILambdaEventHandler<string, DynamoTable>, DynamoDbFunctionEventHandler>();
        }
    }
}