using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using ToDo.Services;

namespace ToDoIntegrationTest
{
    public class APITeste
    {
        private HttpClient _client;
        private TestServer _server;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }
        
        [Test]
        public async Task RetornaAlohaMundoNaRotaPadrão()
        {
            var result = await _client.GetAsync("/");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(result.Content.ReadAsStringAsync().Result, "Aloha Mundo!");
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}