using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.SwaggerUI
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class OperationFilter : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var tag = operation.Tags.FirstOrDefault();
            var route = context.ApiDescription.ActionDescriptor.RouteValues;
            var controller = (ControllerActionDescriptor) context.ApiDescription.ActionDescriptor;
            var attr = controller.ControllerTypeInfo.CustomAttributes.FirstOrDefault(x =>
                x.AttributeType == typeof(SwaggerTagAttribute));
            // Set tag name for each method, if method has the same tag name, it will be grouped in the same group
            if (tag != null)
            {
                tag.Name = $"{route["area"]}/{route["subarea"]}/{route["controller"]}";

                // Read the [SwaggerTag] value
                if (attr != null && !(attr.ConstructorArguments[0].Value?.ToString() ?? string.Empty).IsMissing())
                {
                    tag.Description = attr.ConstructorArguments[0].Value?.ToString();
                    //operation.Description = attr.ConstructorArguments[0].Value?.ToString();
                }

                //Console.WriteLine($"{tag.Name} -> {tag.Description}");
            }
            else
                Console.WriteLine(operation);
        }
    }
}