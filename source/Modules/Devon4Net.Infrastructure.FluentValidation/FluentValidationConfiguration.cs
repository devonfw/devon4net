using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidation<T>(this IServiceCollection services, bool launchExceptionWhenError = false) where T : class
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo != null && !memberInfo.Name.Contains("CustomFluentValidator"))
            {
                throw new ArgumentException($"The provided type {typeof(T).FullName} does not inherit from CustomFluentValidator");
            }

            var objValidator = (T) Activator.CreateInstance(typeof(T), args: launchExceptionWhenError);
            services.AddSingleton(objValidator);
        }
    }
}
