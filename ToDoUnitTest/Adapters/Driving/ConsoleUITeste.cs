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
        private ConsoleUI _console;
        private ServiçoTarefa _serviçoTarefa;
        private ServiçoExportar _serviçoExportar;
        [SetUp]
        public void SetUp()
        {
            _serviçoTarefa = Substitute.For<ServiçoTarefa>(Substitute.For<IFonteDadosTarefas>());
            _serviçoExportar = Substitute.For<ServiçoExportar>(Substitute.For<IFonteDadosTarefas>());
            _console = new ConsoleUI(_serviçoTarefa, _serviçoExportar);
        }
        [Test]
        public void DeveImprimirMensagemDeListaVazia_QuandoNãoHouverTarefas()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);

            _console.MostrarTarefas();
            
            var consoleOutput = saídaDoConsole.ToString();
            consoleOutput.Should().Be("Nenhuma tarefa" + Environment.NewLine);
        }
        
        [Test]
        public void DeveImprimirListaDeTarefasComIds()
        {
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);
            
            _serviçoTarefa
                .ObterTarefas()
                .Returns(new[]
                {
                    new Tarefa(1, "Primeira Tarefa"),
                    new Tarefa(3, "Outra Tarefa")
                });
            
            _console.MostrarTarefas();
            
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

            _serviçoTarefa
                .CriaTarefa("Título da minha tarefa")
                .Returns(new Tarefa(34, "Título da minha tarefa"));

            _console.CriarTarefa();

            saídaDoConsole.ToString().Should().Be("Qual o título da tarefa: " + "Tarefa criada com Id: 34" + Environment.NewLine);
        }
        
        [Test]
        public void DeveMostrarMensagemDeErro_QuandoCriarTarefaComTítuloInválido()
        {
            
            using var entradaDoConsole = new StringReader("       ");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);
            
            _serviçoTarefa
                .CriaTarefa("       ")
                .Throws(new TítuloInválidoExceção());

            _console.CriarTarefa();

            saídaDoConsole.ToString().Should().Be("Qual o título da tarefa: " + "Título inválido para tarefa" + Environment.NewLine);
        }

        [Test]
        public void ExcluirTarefa_DevePerguntarIdEExcluirTarfefa()
        {
            using var entradaDoConsole = new StringReader("1");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);

            _console.ExcluirTarefa();

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
            
            _console.ExcluirTarefa();
            
            _serviçoTarefa.Received().ExcluirTarefa(id);
        }

        [Test]
        public void ExportarTarefas_DevePerguntarQualArquivo()
        {
            //Arrange
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);
            
            //Act
            _console.ExportarTarefas();

            //Assert
            saídaDoConsole.ToString().Should().Be("Arquivo destino: ");
        }

        [Test]
        public void ExportarTarefas_DeveChamarExportadorPassandoArquivo()
        {
            using var entradaDoConsole = new StringReader(@"C:\Temp\meu_arquivo.txt");
            Console.SetIn(entradaDoConsole);

            _console.ExportarTarefas();
            
            _serviçoExportar.Received().Exportar(Arg.Is<IExportador>(x => x.GetType() == typeof(ExportadorArquivo)));
        }

        [Test]
        public void ConcluirTarefa_DevePerguntarIdDaTarefa()
        {
            //Arrange
            using var saídaDoConsole = new StringWriter();
            Console.SetOut(saídaDoConsole);
            
            //Act
            _console.ConcluirTarefa();

            //Assert
            saídaDoConsole.ToString().Should().Be("Tarefa a ser concluída: ");
        }
        
        [Test]
        public void ConcluirTarefa_DeveChamarServiçoParaConcluirTarefaPassandoIdInformado()
        {
            uint id = 1;
            using var entradaDoConsole = new StringReader("1");
            using var saídaDoConsole = new StringWriter();
            Console.SetIn(entradaDoConsole);
            Console.SetOut(saídaDoConsole);
            
            _console.ConcluirTarefa();
            
            _serviçoTarefa.Received().ConcluirTarefa(id);
        }
    }
}