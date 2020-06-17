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
            httpTest.RespondWithJson(new[] {new {id = 1, titulo = "tarefa que existe", concluida = false}});
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            var tarefas = api.ObterTarefas();
            
            httpTest.ShouldHaveCalled("http://localhost:5000/api/Tarefas")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            tarefas.Should().HaveCount(1);
        }

        [Test]
        public void DeveChamarEndpointParaExclusãoDeTarefaPassandoId()
        {
            using var httpTest = new HttpTest();
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            api.ExcluirTarefa(4);

            httpTest.ShouldHaveCalled("http://localhost:5000/api/Tarefas/4")
                .WithVerb(HttpMethod.Delete)
                .Times(1);
        }
        
        [Test]
        public void DeveChamarEndpointDeCriaçãoPassandoTítulo()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new {id = 1, titulo = "Minha tarefa", concluida = false});
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            api.CriarTarefa(new Tarefa("Minha tarefa"));

            httpTest.ShouldHaveCalled("http://localhost:5000/api/Tarefas")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/json")
                .WithRequestBody("{\"titulo\":\"Minha tarefa\"}")
                .Times(1);
        }
        
        [Test]
        public void DeveRetornarTarefaCriada()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWithJson(new {id = 1, titulo = "Minha tarefa", concluida = false});
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            var tarefa = api.CriarTarefa(new Tarefa("Minha tarefa"));

            tarefa.Should().BeEquivalentTo(new {Id = 1, Título = "Minha tarefa"});
            tarefa.EstáConcluída().Should().BeFalse();
        }

        [Test]
        public void DeveChamarEndpointDeConclusãoComId()
        {
            using var httpTest = new HttpTest();
            var api = new TarefasAPI(new HttpClient(new FakeHttpClientMessageHandler()));
            
            api.ConcluirTarefa(3);
            
            httpTest.ShouldHaveCalled("http://localhost:5000/api/Tarefas/3/concluir")
                .WithVerb(HttpMethod.Put)
                .Times(1);
        }
    }
}