using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
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
    }
}