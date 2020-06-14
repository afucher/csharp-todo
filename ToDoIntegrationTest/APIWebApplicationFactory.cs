using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ToDo.Adapters;
using ToDo.Models;
using ToDo.Services;

namespace ToDoIntegrationTest
{
    public class APIWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IFonteDadosTarefas FonteDados { get; private set; } 
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                
                // Remove configuração da aplicação para teste.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(ServiçoTarefa));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                FonteDados = Substitute.For<IFonteDadosTarefas>();
                services.AddScoped<ServiçoTarefa>(provider => new ServiçoTarefa(FonteDados));
            });
        }
    }
}