using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Core.Base;
using Domain.Core.Contracts;
using Expense.API.Application.Services;
using ExpenseManagement.Domain.Infraestructure;
using ExpenseManagement.Domain.Infraestructure.Repositories;
using ExpenseManagement.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Expense.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment environment)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);
            if (environment.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<Startup>();
            }
            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //IoC
            //IoCContainerConfiguration.ConfigureService(services);
            services.AddMvc(option =>
            {

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //DB
            services.AddEntityFrameworkMySql().AddDbContext<ExpenseManagementContext>(options =>
            {
                options.UseMySql(Configuration.GetSection("ConnectionString").Value,
                mySqlOptionsAction: mysqlOpt =>
                {
                    mysqlOpt.MigrationsAssembly(typeof(ExpenseManagementContext).Assembly.GetName().Name);
                });
            }, ServiceLifetime.Scoped);
            services.AddScoped<IDbContext, ExpenseManagementContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<ExpenseService, ExpenseService>();
            //Swagger
            services.AddSwaggerGen(content =>
            {
                content.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info()
                    {
                        Title = "Expenses API",
                        Description = "This API enables you to manage all aspects of expenses.",
                        Version = "v1",
                        TermsOfService = "None"
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
            //
            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase("/api/usermanagement");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Supporting reverse proxy (nginx)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            // Patch path base with forwarded path
            app.Use(async (cont, next) =>
            {
                var forwardedPath = cont.Request.Headers["X-Forwarded-Path"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedPath))
                {
                    cont.Request.PathBase = forwardedPath;
                }

                await next();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            var context = (ExpenseManagementContext)app.ApplicationServices.GetService(typeof(ExpenseManagementContext));
            //if (!context.AllMigrationsApplied())
            //{
            //    context.Database.Migrate();
            //    context.EnsureSeed(app.ApplicationServices.GetService<IOptions<UserSettings>>(),
            //                      app.ApplicationServices.GetService<IHostingEnvironment>(),
            //                      app.ApplicationServices.GetService<ILogger<UserManagementContext>>());
            //}
            //Set Swagger API documentation
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DocumentTitle = "Expenses API";
                config.SwaggerEndpoint("./swagger/v1/swagger.json", "Expenses");
                config.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
        }
    }
}
