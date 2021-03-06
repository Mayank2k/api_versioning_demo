using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace api_versioning_demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddApiVersioning(x =>
            //{
            //    x.DefaultApiVersion = new ApiVersion(1, 0);
            //    x.AssumeDefaultVersionWhenUnspecified = true;
            //    x.ReportApiVersions = true;
            //});
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                //options.SubstituteApiVersionInUrl = true;
                //options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api_versioning_demo", Version = "v1" });
            });

            //services.AddSwaggerGen(options =>
            //{
            //    options.EnableAnnotations();
            //    using (var serviceProvider = services.BuildServiceProvider())
            //    {
            //        var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
            //        String assemblyDescription = typeof(Startup).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
            //        foreach (var description in provider.ApiVersionDescriptions)
            //        {
            //            options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
            //            {
            //                Title = $"{typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
            //                Version = description.ApiVersion.ToString(),
            //                Description = description.IsDeprecated ? $"{assemblyDescription} - DEPRECATED" : $"{assemblyDescription}"
            //            });
            //        }
            //    }

            //    // integrate xml comments  
            //    var currentAssembly = Assembly.GetExecutingAssembly();
            //    var xmlDocs = currentAssembly.GetReferencedAssemblies()
            //    .Union(new AssemblyName[] { currentAssembly.GetName() })
            //    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
            //    .Where(f => File.Exists(f)).ToArray();

            //    Array.ForEach(xmlDocs, (d) =>
            //    {
            //        options.IncludeXmlComments(d);
            //    });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_versioning_demo"));
            //var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            //app.UseSwaggerUI(
            //    options =>
            //    {
            //        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            //    // build a swagger endpoint for each discovered API version  
            //    foreach (var description in provider.ApiVersionDescriptions)
            //            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            //    });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
