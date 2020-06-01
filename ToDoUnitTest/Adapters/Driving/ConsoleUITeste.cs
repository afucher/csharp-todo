using System;
using System.IO;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Services;

namespace ToDoUnitTest.Adapters.Driving
{
    public class ConsoleUITeste
    {
        private ConsoleUI console;
        private ServiçoTarefa serviçoTarefa;
        [SetUp]
        public void SetUp()
        {
            serviçoTarefa = Substitute.For<ServiçoTarefa>(Substitute.For<IFonteDadosTarefas>());
            console = new ConsoleUI(serviçoTarefa);
        }
        [Test]
        public void DeveImprimirMensagemDeListaVazia_QuandoNãoHouverTarefas()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);

            console.MostrarTarefas();
            
            var consoleOutput = saídaDoConsole.ToString();
            consoleOutput.Should().Be("Nenhuma tarefa" + Environment.NewLine);
        }
        
        [Test]
        public void DeveImprimirListaDeTarefasComIds()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);
            
            serviçoTarefa
                .ObterTarefas()
                .Returns(new[]
                {
                    new Tarefa(1, "Primeira Tarefa"),
                    new Tarefa(3, "Outra Tarefa")
                });
            
            console.MostrarTarefas();
            
            var consoleOutput = saídaDoConsole.ToString();
            consoleOutput.Should().Be("[1] - Primeira Tarefa" + Environment.NewLine + "[3] - Outra Tarefa" + Environment.NewLine);
        }

        [Test]
        public void DevePerguntarTítuloDeTarefaECriar()
        {
            using var entradaDoConsole = new StringReader("Título da minha tarefa");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);

            serviçoTarefa
                .CriaTarefa("Título da minha tarefa")
                .Returns(new Tarefa(34, "Título da minha tarefa"));

            console.CriarTarefa();

            saídaDoConsole.ToString().Should().Be("Qual o título da tarefa: " + "Tarefa criada com Id: 34" + Environment.NewLine);
        }
        
        [Test]
        public void DeveMostrarMensagemDeErro_QuandoCriarTarefaComTítuloInválido()
        {
            
            using var entradaDoConsole = new StringReader("       ");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);
            
            serviçoTarefa
                .CriaTarefa("       ")
                .Throws(new TítuloInválidoExceção());

            console.CriarTarefa();

            saídaDoConsole.ToString().Should().Be("Qual o título da tarefa: " + "Título inválido para tarefa" + Environment.NewLine);
        }

        [Test]
        public void ExcluirTarefa_DevePerguntarIdEExcluirTarfefa()
        {
            using var entradaDoConsole = new StringReader("1");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);

            console.ExcluirTarefa();

            saídaDoConsole.ToString().Should().Be("Qual id da tarefa para excluir: " + "Tarefa excluída." + Environment.NewLine);
        }

        [Test]
        public void ExcluirTarefa_DeveChamarServiçoParaExcluirTarefa()
        {
            uint id = 1;
            using var entradaDoConsole = new StringReader("1");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);
            
            console.ExcluirTarefa();
            
            serviçoTarefa.Received().ExcluirTarefa(id);
        }
    }
}