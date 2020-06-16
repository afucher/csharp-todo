using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ToDo.Services
{
    public class APIProgram
    {
        public static void Executa()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                // .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}