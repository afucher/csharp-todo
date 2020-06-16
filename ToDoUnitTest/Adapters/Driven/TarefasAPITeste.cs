using System.Net.Http;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Flurl.Http.Testing;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Models;
using ToDoUnitTest.Helpers;

namespace ToDoUnitTest.Adapters
{
    public class TarefasAPITeste
    {
        [Test]
        public void DeveRetornarListaVazia_QuandoNãoHouverTarefas()
        {
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new Tarefa[] {});
            var tarefas = api.ObterTarefas();

            tarefas.Should().BeEmpty();
        }
        
        [Test]
        public void DeveRetornarTarefasQueExistem()
        {
            using var httpTest = new HttpTest();
            var tarefasDaApi = JsonSerializer.Serialize(
                new[] { new {id = 1, titulo = "tarefa que existe", concluida = false} });
            // httpTest.RespondWithJson(new StringContent(tarefasDaApi, Encoding.Default, "application/json"));
            httpTest.RespondWithJson(new[] {new {id = 1, titulo = "tarefa que existe", concluida = false}});
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            
            var tarefas = api.ObterTarefas();

            
            httpTest.ShouldHaveCalled("http://localhost:5000/api/Tarefas")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            tarefas.Should().HaveCount(1);

        }
    }
}