using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GruzD.Service.Discovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GruzD.Service.Extensions
{
    /// <summary>
    /// Расширения для регистрации документирования Swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiDoc(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = $"API GruzD",
                        Version = new ApiVersion(1, 0).ToString(),
                        Description = "API GruzD"
                    });

                c.DescribeAllParametersInCamelCase();
                c.OrderActionsBy(x => x.RelativePath);

                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(AppContext.BaseDirectory, $"{a.Name}.xml")) // TODO: Check Path.GetDirectoryName(currentAssembly.Location) 
                    .Where(File.Exists).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });

                c.OperationFilter<SwaggerDefaultValues>();

            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiDoc(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(opts =>
            {
                opts.SerializeAsV2 = true;
            })
                .UseSwaggerUI(c =>
                {

                    //c.RoutePrefix = "api-docs";
                    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    //c.DocExpansion(DocExpansion.List);


                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }

                });
            return app;
        }
    }
}