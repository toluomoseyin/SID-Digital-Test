using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UserMgtService.API.Configuration
{
    public static class SwaggerConfiguration
    {
        private static readonly string _bearer = "Bearer";
        private static readonly string _version = "v2";
        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });
                options.SwaggerDoc(_version, CreateInfo());
                options.AddSecurityDefinition(_bearer, CreateScheme());
                options.AddSecurityRequirement(CreateRequirement());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        private static OpenApiSecurityScheme CreateScheme()
        {
            return new OpenApiSecurityScheme()
            {
                Name = "JWT Bearer token",
                Type = SecuritySchemeType.Http,
                Scheme = _bearer,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Bearer token Authorization",
            };
        }

        private static OpenApiSecurityRequirement CreateRequirement()
        {
            return new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = _bearer
                        },
                    },
                    new string[] {}
                }
            };
        }

        private static OpenApiInfo CreateInfo()
        {
            return new OpenApiInfo()
            {
                Version = _version,
            };
        }
    }
}
