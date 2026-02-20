using Commons.DTO.Auth;
using EcomApplication.Service.JWT.Implementaion;
using EcomApplication.Service.JWT.Interface;
using EcomDomain.Entity;
using EcomInfrastructure.DataContext;
using EcomInfrastructure.Repository.Implementation;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace EcomPresentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecomm API",
                    Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer ey12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
        public static IServiceCollection AddCustomDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EcomDbContext>(option => option.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                   .AddEntityFrameworkStores<EcomDbContext>()
                   .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(x =>
            {
                x.User.RequireUniqueEmail = true;
            }
            );

            return services;
        }
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                });
                options.AddPolicy("Filter", policyBuilder =>
                {
                    policyBuilder.WithOrigins(config.GetSection("CORS:AllowedOrigins").Value!.Split(','))
                                .WithMethods(config.GetSection("CORS:AllowedMethods").Value!.Split(','))
                                .WithHeaders(config.GetSection("CORS:AllowedHeaders").Value!.Split(','))
                                .AllowCredentials();
                });
            });

            return services;
        }
        public static IServiceCollection RegisterJwtServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtOptions);

            var issuer = jwtOptions["Issuer"];
            var audience = jwtOptions["Audience"];
            var key = jwtOptions["Key"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
            return services;
        }
    }
}
