namespace MyLibrary.Swagger;

using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


/// <summary>
/// Configures the Swagger options for the API documentation.
/// This class customizes the Swagger documentation by adding a document for each API version, including relevant metadata such as title, version, description, contact information, and license.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The API version description provider.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    /// <summary>
    /// Configures the Swagger options.
    /// </summary>
    /// <param name="options">The SwaggerGen options to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    /// <summary>
    /// Creates the OpenAPI information for a specific API version.
    /// </summary>
    /// <param name="description">The API version description.</param>
    /// <returns>The OpenAPI information for the specified API version.</returns>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Library API",
            Version = description.ApiVersion.ToString(),
            Description = $"Description for Books in the library Web API version {description.ApiVersion.ToString()}",
            Contact = new OpenApiContact { Name = "Samuel Kastberg", Email = "samuel.kastberg@live.se" },
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
