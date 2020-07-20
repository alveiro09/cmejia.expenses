using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Core.Base;
using Domain.Core.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using User.API.Application.Contracts;
using User.API.Application.Model;
using User.API.Application.Services;
using User.API.Infraestructure;
using UserManagement.Domain.Infraestructure;
using UserManagement.Domain.Infraestructure.Repositories;
using UserManagement.Domain.Repositories;

namespace User.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContextPool<UserManagementContext>(
                options => options.UseMySql(Configuration.GetSection("ConnectionString").Value,
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(5, 7), ServerType.MySql);
                    }
            ));
            services.AddScoped<IDbContext, UserManagementContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<UserSettings>(Configuration);
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<ITokenAuthentication, TokenAuthentication>(provider => new TokenAuthentication(Configuration));

            //Swagger
            services.AddSwaggerGen(content =>
            {
                content.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info()
                    {
                        Title = "Users API",
                        Description = "This API enables you to manage all aspects of users.",
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
            //cors
            services.AddCors(setup => setup.AddPolicy("expenses-cors-policy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            }));
            //authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    SaveSigninToken = true
                };
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
            //cors
            app.UseCors("expenses-cors-policy");
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
            if (!context.AllMigrationsApplied())
            {
                context.Database.Migrate();
                context.EnsureSeed(app.ApplicationServices.GetService<IOptions<UserSettings>>(),
                                  app.ApplicationServices.GetService<IHostingEnvironment>(),
                                  app.ApplicationServices.GetService<ILogger<UserManagementContext>>());
            }
            //Set Swagger API documentation
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DocumentTitle = "Users API";
                config.SwaggerEndpoint("./swagger/v1/swagger.json", "Users");
                config.RoutePrefix = string.Empty;
            });
        }
    }
}
