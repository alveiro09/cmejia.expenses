using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Core.Base;
using Domain.Core.Contracts;
using Expense.API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using UserManagement.Domain.Infraestructure;

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
            IoCContainerConfiguration.ConfigureService(services);
            //Swagger
            SwaggerConfiguration.ConfigureServices(services);
            services.AddMvc(option =>
            {

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //DB
            services.AddEntityFrameworkMySql().AddDbContext<UserManagementContext>(options =>
            {
                options.UseMySql(Configuration.GetSection("ConnectionString").Value,
                mySqlOptionsAction: mysqlOpt =>
                {
                    mysqlOpt.MigrationsAssembly(typeof(UserManagementContext).Assembly.GetName().Name);
                });
            }, ServiceLifetime.Scoped);
            services.AddScoped<IDbContext, UserManagementContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
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
            var context = (UserManagementContext)app.ApplicationServices.GetService(typeof(UserManagementContext));
            //if (!context.AllMigrationsApplied())
            //{
            //    context.Database.Migrate();
            //    context.EnsureSeed(app.ApplicationServices.GetService<IOptions<UserSettings>>(),
            //                      app.ApplicationServices.GetService<IHostingEnvironment>(),
            //                      app.ApplicationServices.GetService<ILogger<UserManagementContext>>());
            //}
            //Set Swagger API documentation
            SwaggerConfiguration.Configure(app);
        }
    }
}
