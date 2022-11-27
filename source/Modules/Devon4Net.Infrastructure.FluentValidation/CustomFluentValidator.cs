using Devon4Net.Infrastructure.Common;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace Devon4Net.Infrastructure.FluentValidation
{
    public abstract class CustomFluentValidator<T> : AbstractValidator<T> where T : class
    {
        public abstract void CustomValidate();
        private bool LaunchExceptionWhenError { get; }

        protected CustomFluentValidator(bool launchExceptionWhenError = false)
        {
            LaunchExceptionWhenError = launchExceptionWhenError;
            try
            {
                CallCustomValidationImplementation();
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private void CallCustomValidationImplementation()
        {
            CustomValidate();
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);

            if (result.IsValid) return result;

            var errorMessage = $"Object validation error type {typeof(T).FullName} : {string.Join(",", result.Errors)}";

            Devon4NetLogger.Error(errorMessage);

            if (LaunchExceptionWhenError)
            {
                throw new ArgumentException(errorMessage);
            }

            return result;
        }
    }
}
