﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using NUnit.Framework;
using ToDo.Models;

namespace ToDoIntegrationTest
{
    public class APITeste
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
        public async Task RetornaTarefasNaRotaBaseDeTarefasUsandoController()
        {
            _factory.FonteDados.ObterTarefas().Returns(new Tarefa[] {});
            var result = await _client.GetAsync("/api/tarefas");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            result.Content.ReadAsStringAsync().Result.Should().Be("[]");  
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}