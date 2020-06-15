using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToDo.Adapters;

namespace ToDo.Services
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        
        public Startup(IWebHostEnvironment  env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                // .AddJsonFile("appsettings.json", true, true)
                // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IFonteDadosTarefas>(provider => new TarefasEmMemória());
            services.AddScoped(provider => new ServiçoTarefa(provider.GetService<IFonteDadosTarefas>()));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            // app.UseHttpsRedirection(); 
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {    
                endpoints.MapGet(@"/", 
                    async context => await context.Response.WriteAsync("Aloha Mundo!"));
                endpoints.MapGet(@"/tarefas",
                    async context =>
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("[]");
                    });
                endpoints.MapControllers();
            });
        }

    }
}