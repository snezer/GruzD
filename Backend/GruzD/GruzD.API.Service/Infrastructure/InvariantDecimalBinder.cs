using System;
using System.Globalization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;

namespace GruzD.Web.Infrastructure
{
    public class InvariantDecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return !context.Metadata.IsComplexType && (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
                ? new InvariantDecimalModelBinder(context.Metadata.ModelType)
                : null;
        }
    }

    public class InvariantDecimalModelBinder : IModelBinder
    {
        private readonly SimpleTypeModelBinder _baseBinder;

        public InvariantDecimalModelBinder(Type modelType)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            this._baseBinder = new SimpleTypeModelBinder(modelType, loggerFactory);
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;

                if (decimal.TryParse(valueAsString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal result))
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }

            return this._baseBinder.BindModelAsync(bindingContext);
        }
    }
}
