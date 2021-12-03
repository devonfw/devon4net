using Devon4Net.Infrastructure.Grpc.Attributes;
using System.Reflection;

namespace Devon4Net.Infrastructure.Grpc.Helpers
{
    public static class GrpcServiceHelper
    {
        public static Dictionary<string, Type> GetDevonGrpcServices(string assemblyName)
        {
            var result = new Dictionary<string, Type>();
            var assembly = string.IsNullOrEmpty(assemblyName) ? Assembly.GetExecutingAssembly() : Assembly.Load(assemblyName);
            var ts = assembly.GetTypes().ToList();

            foreach (var item in ts.Where(u => u.GetCustomAttributes(typeof(GrpcDevonServiceAttribute), false).Length > 0))
            {
                result.Add(item.Name, assembly.GetType($"{item.Namespace}.{item.Name}"));
            }

            return result;
        }
    }
}
