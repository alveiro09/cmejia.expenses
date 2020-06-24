using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Expense.API.Configurations
{
    /// <summary>
    /// Swagger API documentation components start-up configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Configures swagger gen service.
        /// </summary>
        /// <param name="services">The application services.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(content =>
            {
                content.SwaggerDoc("V1",
                    new Info()
                    {
                        Title = "Users API",
                        Description = "This API provide users management process.",
                        Version = "v1",
                    });
                content.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                content.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                content.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DocumentTitle = "User API";
                config.SwaggerEndpoint("swagger/V1/swagger.json", "Expenses");
                config.RoutePrefix = string.Empty;
            });
        }
    }
}
