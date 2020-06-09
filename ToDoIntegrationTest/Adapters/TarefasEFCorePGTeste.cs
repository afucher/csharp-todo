using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Models;

namespace ToDoIntegrationTest.Adapters
{
    public class TarefasEFCorePGTeste
    {
        private static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=todo_test";

        private TarefasDbContext contexto;

        [SetUp]
        public void SetUp()
        {
            contexto = new TarefasDbContext(new DbContextOptionsBuilder().UseNpgsql(parametrosConexão).Options);
            contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE public.tarefas");
        }

        [Test]
        public void DeveRetornarListaVaziaDeTarefas()
        {
            var tarefasEFCore = new TarefasEFCorePG(contexto);

            var tarefas = tarefasEFCore.ObterTarefas();

            tarefas.Should().BeEmpty();
        }
        
        [Test]
        public void DeveRetornarListaComJáExistentes()
        {
            var tarefasEFCore = new TarefasEFCorePG(contexto);
            var id = 1;
            var titulo = "meu título";
            var concluída = true;

            contexto.Database.ExecuteSqlInterpolated(
                $"insert into public.tarefas(id, titulo, concluida) values ({id}, {titulo}, {concluída})");
            
            var tarefas = tarefasEFCore.ObterTarefas();

            tarefas.Should().BeEquivalentTo(new { Id=1, Título="meu título"});
            tarefas.First().EstáConcluída().Should().BeTrue();
        }
        
        [Test]
        public void DeveCriarTarefaComId1ENãoConcluída()
        {
            var tarefasEFCore = new TarefasEFCorePG(contexto);

            tarefasEFCore.CriarTarefa(new Tarefa("tarefa para ser criada"));

            var tarefas = contexto.Tarefas
                .FromSqlRaw(@"select * from public.tarefas
                                        where id = 1 AND 
                                              titulo = 'tarefa para ser criada' AND 
                                              concluida = false")
                .ToList();

            tarefas.Should().HaveCount(1);
        }
        
        [Test]
        public void DeveCriarTarefasComIdSequencial()
        {
            var tarefasEFCore = new TarefasEFCorePG(contexto);

            var tarefa1 = tarefasEFCore.CriarTarefa(new Tarefa("tarefa para ser criada"));
            var tarefa2 = tarefasEFCore.CriarTarefa(new Tarefa("segunda tarefa"));
            var tarefa3 = tarefasEFCore.CriarTarefa(new Tarefa("terceira tarefa"));

            tarefa1.Id.Should().Be(1);
            tarefa2.Id.Should().Be(2);
            tarefa3.Id.Should().Be(3);
        }
    }
}