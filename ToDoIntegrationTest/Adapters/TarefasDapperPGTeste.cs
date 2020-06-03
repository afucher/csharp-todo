using System.Linq;
using Dapper;
using FluentAssertions;
using Npgsql;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Models;

namespace ToDoIntegrationTest.Adapters
{
    public class TarefasDapperPGTeste
    {
        private static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=todo_test";

        [SetUp]
        public void SetUp()
        {
            var conexão = new NpgsqlConnection(parametrosConexão);
            conexão.Execute("delete from public.tarefas");
        }
        
        [Test]
        public void DeveRetornarListaVaziaDeTarefas()
        {
            var tarefasDapper = new TarefasDapperPG(new NpgsqlConnection(parametrosConexão));

            var tarefas = tarefasDapper.ObterTarefas();

            tarefas.Should().BeEmpty();
        }
        
        [Test]
        public void DeveCriarERetornarListaComTarefaCriada()
        {
            var conexão = new NpgsqlConnection(parametrosConexão); 
            var tarefasDapper = new TarefasDapperPG(conexão);
            
            var count = conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[] { new { Id=1, Título="meu título", Concluída=true }}
            );
            
            var tarefas = tarefasDapper.ObterTarefas();

            tarefas.Should().BeEquivalentTo(new { Id=1, Título="meu título"});
            tarefas.First().EstáConcluída().Should().BeTrue();
        }
    }
}