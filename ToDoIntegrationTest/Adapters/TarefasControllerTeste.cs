using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using NUnit.Framework;
using ToDo.Models;

namespace ToDoIntegrationTest
{
    public class TarefasControllerTeste
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        
        [Test]
        public async Task RetornaAlohaMundoNaRotaPadrão()
        {
            var result = await _client.GetAsync("/");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.ReadAsStringAsync().Result.Should().Be("Aloha Mundo!");
        }

        [Test]
        public async Task RetornaTarefasNaRotaBaseDeTarefasSemUsarController()
        {
            var result = await _client.GetAsync("/tarefas");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            result.Content.ReadAsStringAsync().Result.Should().Be("[]");  
        }
        
        [Test]
        public async Task RetornaArrayVazioNaRotaBaseDeTarefasUsandoController_QuandoNãoExisteTarefas()
        {
            _factory.FonteDados.ObterTarefas().Returns(new Tarefa[] {});
            var result = await _client.GetAsync("/api/tarefas");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            result.Content.ReadAsStringAsync().Result.Should().Be("[]");  
        }
        
        [Test]
        public async Task RetornaTarefasNaRotaBaseDeTarefasUsandoController()
        {
            _factory.FonteDados.ObterTarefas().Returns(new[]
            {
                new Tarefa(1, "primeira tarefa", false),
                new Tarefa(2, "segunda tarefa", true)
            });
            var valorEsperado = JsonSerializer.Serialize(new []
            {
                new {id = 1, titulo = "primeira tarefa", concluida = false},
                new {id = 2, titulo = "segunda tarefa", concluida = true}
            });
            
            var result = await _client.GetAsync("/api/tarefas");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            result.Content.ReadAsStringAsync().Result.Should().Be(valorEsperado);  
        }
        
        [Test]
        public async Task CriaTarefaERetornaTarefaCriada()
        {
            var valorEsperado = JsonSerializer.Serialize(new {id = 1, titulo = "tarefa nova", concluida = false});
            _factory.FonteDados
                .CriarTarefa(Arg.Is<Tarefa>(tarefa => tarefa.Título.Equals("tarefa nova")))
                .Returns(new Tarefa(1, "tarefa nova"));

            var conteúdo = JsonSerializer.Serialize(new {titulo = "tarefa nova"});
            var result = await _client.PostAsync("/api/tarefas",new StringContent(conteúdo, Encoding.Default, "application/json"));
            
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            result.Content.ReadAsStringAsync().Result.Should().Be(valorEsperado);
        }

        [Test]
        public async Task CriaTarefaComTítuloInválidoRetorna422()
        {
            var conteúdo = JsonSerializer.Serialize(new {titulo = ""});
            var result = await _client.PostAsync("/api/tarefas",new StringContent(conteúdo, Encoding.Default, "application/json"));
            
            result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}