using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Devon4Net.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidation<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            var memberInfo = typeof(TImplementation).BaseType;
            if (memberInfo?.Name.Contains("CustomFluentValidator") == false)
            {
                throw new ArgumentException($"The provided type {typeof(TImplementation).FullName} does not inherit from CustomFluentValidator");
            }

            services.AddTransient<TService,TImplementation>();
        }
    }
}
