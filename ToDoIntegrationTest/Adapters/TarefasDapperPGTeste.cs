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
        
        [Test]
        public void DeveCriarTarefasComIdSequencial()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);

            var tarefa1 = tarefasDapper.CriarTarefa(new Tarefa("tarefa para ser criada"));
            var tarefa2 = tarefasDapper.CriarTarefa(new Tarefa("segunda tarefa"));
            var tarefa3 = tarefasDapper.CriarTarefa(new Tarefa("terceira tarefa"));

            tarefa1.Id.Should().Be(1);
            tarefa2.Id.Should().Be(2);
            tarefa3.Id.Should().Be(3);
        }

        [Test]
        public void DeveExcluirTarefa()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);
            uint idParaExcluir = 1;
            _conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[]
                {
                    new { Id=1, Título="meu título", Concluída=true },
                    new { Id=2, Título="segunda tarefa", Concluída=true }
                }
            );
            
            tarefasDapper.ExcluirTarefa(idParaExcluir);
            
            var tarefas = _conexão
                .Query(@"select id, titulo from public.tarefas")
                .Select(item => new {id = item.id, titulo = item.titulo});
            
            tarefas.Should().BeEquivalentTo(new {id = 2, titulo = "segunda tarefa"});
        }

        [Test]
        public void ExcluirTarefaNãoDeveFazerNada_QuandoIdNãoExistir()
        {
            var tarefasDapper = new TarefasDapperPG(_conexão);
            uint idParaExcluir = 3;
            _conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[]
                {
                    new { Id=1, Título="meu título", Concluída=true },
                    new { Id=2, Título="segunda tarefa", Concluída=true }
                }
            );
            
            tarefasDapper.ExcluirTarefa(idParaExcluir);
            
            var tarefas = _conexão
                .Query(@"select id, titulo from public.tarefas")
                .Select(item => new {id = item.id, titulo = item.titulo});
            
            tarefas.Should().BeEquivalentTo(
                new {id = 1, titulo = "meu título"},
                                new {id = 2, titulo = "segunda tarefa"});
        }
    }
}