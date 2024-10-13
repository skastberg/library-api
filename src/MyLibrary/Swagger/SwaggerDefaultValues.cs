namespace MyLibrary.Swagger;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;


/// <summary>
/// A filter that applies default values to Swagger operations.  
/// This class customizes the Swagger documentation by setting default values, descriptions, and required flags for parameters and responses.
/// </summary>
public class SwaggerDefaultValues : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified operation.
    /// </summary>
    /// <param name="operation">The operation to apply the filter to.</param>
    /// <param name="context">The context for the filter.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();

        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach (var contentType in response.Content.Keys)
            {
                if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                {
                    response.Content.Remove(contentType);
                }
            }
        }

        if (operation.Parameters == null)
        {
            return;
        }

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            parameter.Description ??= description.ModelMetadata?.Description;

            if (parameter.Schema.Default == null &&
                 description.DefaultValue != null &&
                 description.DefaultValue is not DBNull &&
                 description.ModelMetadata is ModelMetadata modelMetadata)
            {
                var json = JsonSerializer.Serialize(description.DefaultValue, modelMetadata.ModelType);
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            }

            parameter.Required |= description.IsRequired;
        }
    }
}
