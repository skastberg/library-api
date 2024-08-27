using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyLibrary.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



builder.Services.AddApiVersioning(opt =>
    {
        var builder = new MediaTypeApiVersionReaderBuilder();

        opt.ApiVersionReader = builder.Parameter("v")
                                          .Include("application/json")
                                          .Build();
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(2, 0);
        opt.ReportApiVersions = true;

        opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
    }
).AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = false;
    opt.ApiVersionParameterSource = new MediaTypeApiVersionReader();
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
  
        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.ApiVersion.ToString();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
