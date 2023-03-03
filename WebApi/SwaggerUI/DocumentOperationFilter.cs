using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.SwaggerUI
{
    /// <inheritdoc cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    [ExcludeFromCodeCoverage]
    public class DocumentOperationFilter : IDocumentFilter, IOperationFilter
    {
        private static readonly List<OpenApiTag> Tags = new List<OpenApiTag>();

        /// <inheritdoc />
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags?.Clear(); // Just clear all tags then build a new one by using OperationFilter
            foreach (var tag in Tags.OrderBy(t => t.Name).ToList())
            {
                swaggerDoc.Tags?.Add(tag);
            }
        }

        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Tags?.Clear();
            var tag = new OpenApiTag();
            var route = context.ApiDescription.ActionDescriptor.RouteValues;
            var controller = (ControllerActionDescriptor) context.ApiDescription.ActionDescriptor;
            var attr = controller.ControllerTypeInfo.CustomAttributes.FirstOrDefault(x =>
                x.AttributeType == typeof(SwaggerTagAttribute));
            // Set tag name for each method, if method has the same tag name, it will be grouped in the same group
            tag.Name = $"{route["area"]}/{route["subarea"]}/{route["controller"]}";

            // Read the [SwaggerTag] value
            if (attr != null && !(attr.ConstructorArguments[0].Value?.ToString() ?? string.Empty).IsMissing())
            {
                tag.Description = attr.ConstructorArguments[0].Value?.ToString();
            }

            operation.Tags?.Add(tag);
            Tags.Add(tag);
        }
    }
}