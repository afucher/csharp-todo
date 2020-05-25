using FluentAssertions;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Models;

namespace ToDoUnitTest.Adapters
{
    public class TarefasEmMemóriaTeste
    {
        [Test]
        public void DeveRetornarNenhumaTarefa_QuandoInicializado()
        {
            var tarefasEmMemória = new TarefasEmMemória();

            var tarefas = tarefasEmMemória.ObterTarefas();

            tarefas.Should().BeEmpty();
        }

        [Test]
        public void DeveCriarTarefaERetornarComIdSendoUmNaPrimeiraTarefa()
        {
            var tarefasEmMemória = new TarefasEmMemória();

            var tarefa = tarefasEmMemória.CriarTarefa(new Tarefa("título somente"));
            
            tarefa.Should().BeEquivalentTo(new Tarefa(1, "título somente"));
        }

        [Test]
        public void ObterTarefasDeveRetornarTarefasCriadas()
        {
            var tarefasEmMemória = new TarefasEmMemória();
            tarefasEmMemória.CriarTarefa(new Tarefa("título somente"));
            tarefasEmMemória.CriarTarefa(new Tarefa("título aleatório mas fixo"));

            var tarefas = tarefasEmMemória.ObterTarefas();

            tarefas.Should().BeEquivalentTo(
                new {Id = 1, Título = "título somente"}, 
                                new {Id = 2, Título = "título aleatório mas fixo"});
        }

        [Test]
        public void TarefasDevemSerCriadasComIdSequencial()
        {
            var tarefasEmMemória = new TarefasEmMemória();
            var tarefa1 = tarefasEmMemória.CriarTarefa(new Tarefa("tarefa 1"));
            var tarefa2 = tarefasEmMemória.CriarTarefa(new Tarefa("tarefa 2"));
            var tarefa3 = tarefasEmMemória.CriarTarefa(new Tarefa("tarefa 3"));

            tarefa1.Id.Should().Be(1);
            tarefa2.Id.Should().Be(2);
            tarefa3.Id.Should().Be(3);
        }
    }
}