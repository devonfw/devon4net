using System;
using Devon4Net.Infrastructure.Log;
using FluentValidation;
using FluentValidation.Results;

namespace Devon4Net.Infrastructure.FluentValidation
{
    public abstract class CustomFluentValidator<T> : AbstractValidator<T> where T : class
    {
        private bool LaunchExceptionWhenError { get; }

        protected CustomFluentValidator(bool launchExceptionWhenError)
        {
           LaunchExceptionWhenError = launchExceptionWhenError;
           CustomValidate();
        }

        public abstract void CustomValidate();

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (result.IsValid) return result;

            var errorMessage = $"Error validating object type {typeof(T).FullName} : {string.Join(",", result.Errors)}";
            
            Devon4NetLogger.Error(errorMessage);

            if (LaunchExceptionWhenError)
            {
                throw new ArgumentException(errorMessage);
            }

            return result;
        }
    }
}
