﻿// Copyright (c) 2018 Inventory Innovations, Inc. - build by Jon P Smith (GitHub JonPSmith)
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Reflection;
using Devon4Net.Infrastructure.Common.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    /// <summary>
    /// This contains the extension methods for registering classes automatically
    /// </summary>
    public static class AutoRegisterHelpers
    {
        /// <summary>
        /// This finds all the public, non-generic, non-nested classes in an assembly in the provided assemblies.
        /// If no assemblies provided then it scans the assembly that called the method
        /// </summary>
        /// <param name="services">the NET Core dependency injection service</param>
        /// <param name="assemblies">Each assembly you want scanned. If null then scans the the assembly that called the method</param>
        /// <returns></returns>
        public static AutoRegisterData RegisterAssemblyPublicNonGenericClasses(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = new[] {Assembly.GetCallingAssembly()};

            var allPublicTypes = assemblies.SelectMany(x => x.GetExportedTypes()
                .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested));

            return new AutoRegisterData(services, allPublicTypes);
        }

        /// <summary>
        /// This allows you to filter the classes in some way.
        /// For instance <code>Where(c =\> c.Name.EndsWith("Service")</code> would only register classes who's name ended in "Service"
        /// </summary>
        /// <param name="autoRegData"></param>
        /// <param name="predicate">A function that will take a type and return true if that type should be included</param>
        /// <returns></returns>
        public static AutoRegisterData Where(this AutoRegisterData autoRegData, Func<Type, bool> predicate)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            autoRegData.TypeFilter = predicate;
            return new AutoRegisterData(autoRegData.Services, autoRegData.TypesToConsider.Where(predicate));
        }

        /// <summary>
        /// This registers the classes against any public interfaces (other than IDisposable) implemented by the class
        /// </summary>
        /// <param name="autoRegData">AutoRegister data produced by <see cref="RegisterAssemblyPublicNonGenericClasses"/></param> method
        /// <param name="lifetime">Allows you to define the lifetime of the service - defaults to ServiceLifetime.Transient</param>
        /// <returns></returns>
        public static IServiceCollection AsPublicImplementedInterfaces(this AutoRegisterData autoRegData, 
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            foreach (var classType in (autoRegData.TypeFilter == null 
                ? autoRegData.TypesToConsider 
                : autoRegData.TypesToConsider.Where(autoRegData.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces;
                foreach (var infc in interfaces.Where(i => i != typeof(IDisposable) && i.IsPublic && !i.IsNested))
                {
                    autoRegData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                }
            }

            return autoRegData.Services;
        }

        /// <summary>
        /// This registers the classes against any public class (other than IDisposable) implemented by the class as a Singleton instance
        /// </summary>
        /// <param name="autoRegData">AutoRegister data produced by <see cref="RegisterAssemblyPublicNonGenericClasses"/></param> method
        /// <returns></returns>
        public static IServiceCollection AsSingletonPublicImplementedClasses(this AutoRegisterData autoRegData)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            foreach (var classType in (autoRegData.TypeFilter == null
                ? autoRegData.TypesToConsider
                : autoRegData.TypesToConsider.Where(autoRegData.TypeFilter)))
            {
                autoRegData.Services.AddSingleton(classType);
            }

            return autoRegData.Services;
        }

        public static void AutoRegisterClasses(this IServiceCollection services, List<Type> assemblyContainerToScan, string sufixName = "Service")
        {
            if (assemblyContainerToScan == null || assemblyContainerToScan.Count == 0|| string.IsNullOrEmpty(sufixName)) return;

            foreach (var assembly in assemblyContainerToScan)
            {
                var assemblyToScan = Assembly.GetAssembly(assembly);
                if (assemblyToScan == null) continue;

                services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.EndsWith(sufixName))
                .AsPublicImplementedInterfaces();
            }
        }

        public static void AutoRegisterClasses(this IServiceCollection services, List<Type> assemblyContainerToScan, List<string> sufixNames)
        {
            if (assemblyContainerToScan == null || assemblyContainerToScan.Count == 0 || sufixNames == null || sufixNames.Count == 0) return;

            foreach (var assembly in assemblyContainerToScan)
            {
                var assemblyToScan = Assembly.GetAssembly(assembly);
                if (assemblyToScan == null) continue;

                foreach (var sufixName in sufixNames)
                {
                    services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                    .Where(x => x.Name.EndsWith(sufixName))
                    .AsPublicImplementedInterfaces();
                }
            }
        }
    }
}
