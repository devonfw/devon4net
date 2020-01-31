using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using AOP.ErrorHandler.Core.Common.Interceptor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.DependencyModel;

namespace AOP.ErrorHandler.Core.Common.Handler
{
    public static class CastleWindsorAopRegister
    {
        private static readonly string PathApp = System.IO.Directory.GetCurrentDirectory();
        public static WindsorContainer SetUpWindsor()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ExceptionHandler>().Named("ExceptionHandler"));
            RegisterAssemblies(ref container);            
            container.Kernel.ProxyFactory.AddInterceptorSelector(new ExceptionInterceptor());
            return container;
        }



        private static void RegisterAssemblies(ref WindsorContainer aContainer)
        {
            var assembliesToReg = GetAssembliesToRegister();
            if (!assembliesToReg.Any()) return;

            var assemblies = Assembly.GetEntryAssembly();
            var assemblyNameList = assemblies.GetReferencedAssemblies();            

            foreach (var assemblyItem in assembliesToReg)
            {
                var assemblyName = assemblyNameList.FirstOrDefault(n => n.Name.ToLower() == assemblyItem.ToLower());
                var interfaces = Assembly.Load(assemblyName).ExportedTypes;
                foreach (var interfaceItem in interfaces)
                {
                    aContainer.Register(new ComponentRegistration(interfaceItem));
                }
            }
        }


        private static List<String> GetAssembliesToRegister()
        {
            var fileXml = "AOPAssemblies.xml";
            var path = Path.Combine(PathApp, fileXml); // PathApp + @"\" +  fileXml;

            if (!File.Exists(@path)) return null;

            var doc = XDocument.Load(@path);
            return doc.Root != null
                ? doc.Root.Elements("Assembly")
                    .Select(element => element.Value)
                    .ToList()
                : null;
        }

        public static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateLibrary(library, assemblyName))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == (assemblyName)
                || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }

        public static Assembly[] GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies.ToArray();
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Name == ("Specify")
                || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Specify"));
        }


    }
}
