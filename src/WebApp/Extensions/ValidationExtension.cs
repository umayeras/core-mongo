using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Validation;
using WebApp.Validation.Abstract;
using WebApp.Validation.RequestValidators;

namespace WebApp.Extensions
{
    internal static class ValidationExtension
    {
        internal static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<AddSampleRequestValidator>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddSingleton<IRequestValidator, RequestValidator>();
        }
    }
}