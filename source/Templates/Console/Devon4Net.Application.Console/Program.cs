// See https://aka.ms/new-console-template for more information

using Devon4Net.Application.Console;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

var consoleExtension = new ConsoleExtension();
consoleExtension.GetConfigurationObjects(out var configuration, serviceCollection: out IServiceCollection serviceCollection);

Devon4NetLogger.Debug("Hello, World!");
