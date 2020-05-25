using System;
using System.IO;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Models;
using ToDo.Services;

namespace ToDoUnitTest.Adapters.Driving
{
    public class ConsoleUITeste
    {
        [Test]
        public void DeveImprimirMensagemDeListaVazia_QuandoNãoHouverTarefas()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);
            var console = new ConsoleUI(new ServiçoTarefa(new TarefasEmMemória()));

            console.MostrarTarefas();
            
            var consoleOutput = saídaDoConsole.ToString();
            consoleOutput.Should().Be("Nenhuma tarefa" + Environment.NewLine);
        }
        
        [Test]
        public void DeveImprimirListaDeTarefasComIds()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);

            var serviçoTarefa = Substitute.For<ServiçoTarefa>(Substitute.For<IFonteDadosTarefas>());
            serviçoTarefa
                .ObterTarefas()
                .Returns(new[]
                {
                    new Tarefa(1, "Primeira Tarefa"),
                    new Tarefa(3, "Outra Tarefa")
                });
            
            var console = new ConsoleUI(serviçoTarefa);

            console.MostrarTarefas();
            
            var consoleOutput = saídaDoConsole.ToString();
            consoleOutput.Should().Be("[1] - Primeira Tarefa" + Environment.NewLine + "[3] - Outra Tarefa" + Environment.NewLine);
        }
    }
}