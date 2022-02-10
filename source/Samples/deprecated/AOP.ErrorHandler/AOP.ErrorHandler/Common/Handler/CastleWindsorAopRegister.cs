using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using AOP.ErrorHandler.Common.Interceptor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AOP.ErrorHandler.Common.Handler
{
    public static class CastleWindsorAopRegister
    {
        private static readonly string PathApp = System.IO.Directory.GetCurrentDirectory();
        public static WindsorContainer SetUpWindsor()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ExceptionHandler>().Named("ExceptionHandler"));
            RegisterAssemblies(ref container);
            //RegisterAssembliesOriginal(ref container);
            //RegisterInterfaces(ref container);
            container.Kernel.ProxyFactory.AddInterceptorSelector(new ExceptionInterceptor());
            return container;
        }



        private static void RegisterAssemblies(ref WindsorContainer aContainer)
        {
            var assembliesList = GetAssembliesToRegister().ToArray();

            if (!assembliesList.Any()) return;
    
            foreach (var dll in assembliesList)
            {
                var assembly = Assembly.LoadFile(Path.Combine(PathApp, dll));
                var interfaces = assembly.GetTypes().Where(type => type.IsInterface && type.IsPublic).ToList();
                foreach (var interfaceItem in interfaces)
                {
                    var info = interfaceItem.GetTypeInfo();
                    aContainer.Register(new ComponentRegistration(info));
                }

            }           
        }

        private static void RegisterInterfaces(ref WindsorContainer aContainer)
        {
            var interfacesList = GetInterfacesToRegister().ToArray();
            if (!interfacesList.Any()) return;
            var rootPath = System.IO.Directory.GetCurrentDirectory();
            foreach (var interfaces in from interfaceObj in interfacesList where !string.IsNullOrEmpty(interfaceObj.Key) select Assembly.Load(interfaceObj.Value)
                .GetTypes()
                .Where(type => type.IsInterface && type.IsPublic && type.Name == interfaceObj.Key)
                .ToList())
            {
                aContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(rootPath))
                    .Where(x => x.GetInterfaces().Intersect(interfaces).Any())
                    .LifestyleTransient()
                    .WithService.DefaultInterfaces());
            }
        }


        private static Dictionary<string, string> GetInterfacesToRegister()
        {
            var fileXml = "AOPIterfaces.xml";
            var path = Path.Combine(PathApp, fileXml);
            //var path = PathApp + fileXml;
            var values = new Dictionary<string, string>();
            if (!File.Exists(@path)) return null;

            var doc = new XmlDocument();
            doc.Load(path);

            var elementList = doc.GetElementsByTagName("property");

            for (var i = 0; i < elementList.Count; i++)
            {
                var xmlAttributeCollection = elementList[i].Attributes;
                if (xmlAttributeCollection == null) continue;
                var key = xmlAttributeCollection["interface"].Value;
                var value = xmlAttributeCollection["assembly"].Value;
                values.Add(key, value);
            }

            return values;
        }

        private static IEnumerable<String> GetAssembliesToRegister()
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
    }
}
