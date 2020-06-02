using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Services;

namespace ToDoUnitTest.Services
{
    public class ServiçoTarefaTeste
    {
        [Test]
        public void DeveTrazerNenhumaTarefa_QuandoFonteDeDadosNãoTiverDados()
        {
            
            //Arrange
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);

            //Act
            var tarefas = serviço.ObterTarefas();
            
            //Assert
            tarefas.Should().BeEmpty();
        }

        [Test]
        public void DeveRetornarTodasTarefas()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var tarefa = new Tarefa("Tarefa 1");
            fonteDeDados.ObterTarefas().Returns(new []
            {
                tarefa
            }); 
            var serviço = new ServiçoTarefa(fonteDeDados);
            
            var tarefas = serviço.ObterTarefas();

            tarefas.Should().BeEquivalentTo(tarefa);
        }

        [Test]
        public void CriarTarefa_DeveRetornarTarefaCriada()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);

            fonteDeDados
                .CriarTarefa(Arg.Is<Tarefa>(tarefa => tarefa.Título.Equals("título")))
                .Returns(new Tarefa(1, "título"));

            var tarefa = serviço.CriaTarefa("título");

            tarefa.Should().BeEquivalentTo(new Tarefa(1,"título"));
        }

        [Test]
        public void CriarTarefa_DeveCriarTarefaNaFonteDeDados()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);

            serviço.CriaTarefa("título");

            fonteDeDados.Received().CriarTarefa(Arg.Is<Tarefa>(tarefa => tarefa.Título.Equals("título") && !tarefa.EstáConcluída()));
        }

        [Test]
        public void CriarTarefa_DeveLançarExceção_QuandoTítuloForInválido()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);

            Assert.Throws<TítuloInválidoExceção>(() => serviço.CriaTarefa(""));
        }

        [Test]
        public void ExcluirTarefa_DeveExcluirTarefa()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);
            uint id = 2;

            serviço.ExcluirTarefa(2);

            fonteDeDados.Received().ExcluirTarefa(2);
        }

        [Test]
        public void ConcluirTarefa_DeveConcluirTarefaPassandoId()
        {
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var serviço = new ServiçoTarefa(fonteDeDados);
            uint id = 2;

            serviço.ConcluirTarefa(2);

            fonteDeDados.Received().ConcluirTarefa(2);
        }
    }
}