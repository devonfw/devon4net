// See https://aka.ms/new-console-template for more information
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Consumers;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var awsc = new AwsConsoleExtension();
awsc.GetConfigurationObjects(out var Configuration, out IServiceCollection ServiceCollection);


using var sp = ServiceCollection.BuildServiceProvider();
var sqsSample = sp.GetService<SqsSample>();
var sqsConsumerSample = sp.GetService<SqsConsumerSample>();
_ = Task.Factory.StartNew(async () => await sqsSample.StartSending().ConfigureAwait(true));
await sqsConsumerSample.Start().ConfigureAwait(false);
