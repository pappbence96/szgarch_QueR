using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NSwag;
using QueR.Application.Hubs;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.Application.Services;
using QueR.BLL;
using QueR.BLL.Services.Company;
using QueR.BLL.Services.Identity;
using QueR.BLL.Services.Queue;
using QueR.BLL.Services.QueueType;
using QueR.BLL.Services.Site;
using QueR.BLL.Services.Ticket;
using QueR.BLL.Services.User;
using QueR.DAL;
using QueR.Domain.Entities;
using QueR.Domain.Services;

namespace QueR.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "http://localhost:4300")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            })
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = "http://localhost:5001",
                    ClockSkew = TimeSpan.Zero,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    }
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddHttpContextAccessor();
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<IQueueTypeService, QueueTypeService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddOpenApiDocument(config =>
            {
                config.Title = "QueR Backoffice API";
                config.Description = "QueR Backoffice API documentation for development purposes.";
                config.DocumentName = "Backoffice";
                config.ApiGroupNames = new[] { "backoffice", "common" };
                config.UseRouteNameAsOperationId = true;

                config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
            });
            services.AddOpenApiDocument(config =>
            {
                config.Title = "QueR Mobile API";
                config.Description = "QueR Mobile API documentation for development purposes.";
                config.DocumentName = "Mobile";
                config.ApiGroupNames = new[] { "mobile", "common" };
                config.UseRouteNameAsOperationId = true;

                config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
            });

            services.AddAutoMapper(Assembly.Load("QueR.BLL"));

            services.AddSignalR();

            services.AddControllers()
                .AddNewtonsoftJson(opts => {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandlingMiddleware();
            app.UseCors("EnableCORS");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<QueueHub>("hubs/queue");
                endpoints.MapHub<TicketHub>("hubs/ticket");
            });
        }
    }
}
