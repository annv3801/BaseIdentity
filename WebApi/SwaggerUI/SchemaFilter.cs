using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
#pragma warning disable 8600
#pragma warning disable 8604

namespace WebApi.SwaggerUI
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class SchemaFilter : ISchemaFilter
    {
        private string ToCamelCase(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        private string GetType(object c)
        {
            switch (Type.GetTypeCode(c.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return "integer";
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return "number";
                case TypeCode.String:
                    return "string";
                case TypeCode.Boolean:
                    return "boolean";
                default:
                    return "object";
            }
        }

        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (PropertyInfo propertyInfo in context.Type.GetProperties())
            {
                // Look for class attributes that have been decorated with "[DefaultAttribute(...)]".
                DefaultValueAttribute defaultAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

                if (defaultAttribute != null)
                {
                    foreach (KeyValuePair<string, OpenApiSchema> property in schema.Properties)
                    {
                        // Only assign default value to the proper element.
                        if (ToCamelCase(propertyInfo.Name) == property.Key)
                        {
                            var value = OpenApiAnyFactoryExtensions.CreateFor(new OpenApiSchema()
                            {
                                Type = GetType(defaultAttribute.Value),
                            }, defaultAttribute.Value);
                            property.Value.Example = value;
                            break;
                        }
                    }
                }
            }
        }
    }
}