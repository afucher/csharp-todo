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

        private NpgsqlConnection _conexão;

        [SetUp]
        public void SetUp()
        {
            _conexão = new NpgsqlConnection(parametrosConexão);
            _conexão.Execute("delete from public.tarefas");
        }
        
        [Test]
        public void DeveRetornarListaVaziaDeTarefas()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);

            var tarefas = tarefasDapper.ObterTarefas();

            tarefas.Should().BeEmpty();
        }
        
        [Test]
        public void DeveRetornarListaComJáExistentes()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);
            
            var count = _conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[] { new { Id=1, Título="meu título", Concluída=true }}
            );
            
            var tarefas = tarefasDapper.ObterTarefas();

            tarefas.Should().BeEquivalentTo(new { Id=1, Título="meu título"});
            tarefas.First().EstáConcluída().Should().BeTrue();
        }

        [Test]
        public void DeveCriarTarefaComId1ENãoConcluída()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);

            tarefasDapper.CriarTarefa(new Tarefa("tarefa para ser criada"));

            var tarefas = _conexão
                .Query(@"select * from public.tarefas
                                        where id = 1 AND 
                                              titulo = 'tarefa para ser criada' AND 
                                              concluida = false");

            tarefas.Should().HaveCount(1);
        }
    }
}