using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ToDo.Adapters;

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
    }
}