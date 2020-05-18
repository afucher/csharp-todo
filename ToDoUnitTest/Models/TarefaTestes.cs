using System;
using NUnit.Framework;
using ToDo.Exceptions;
using ToDo.Models;

namespace ToDoUnitTest.Models
{
    public class TarefaTestes
    {
        [Test]
        public void ConstrutorDaTarefaDeveReceberValoresIniciais()
        {
            var tarefa = new Tarefa("Descobrir pq Limited WIP não funciona");

            Assert.AreEqual("Descobrir pq Limited WIP não funciona", tarefa.Título);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void ConstrutorNãoDeveAceitarTítuloNuloOuVazio(string título)
        {
            Assert.Throws<TítuloInválidoExceção>(() => new Tarefa(título));
        }

        [Test]
        public void TarefaDeveSerNãoConcluídaQuandoCriada()
        {
            var tarefa = new Tarefa("Qualquer");
            
            Assert.False(tarefa.EstáConcluída());
        }

        [Test]
        public void ConcluirTarefaDeveDeixarTarefaComoConcluída()
        {
            //Arrange
            var tarefa = new Tarefa("Finalizada");

            //Act
            tarefa.Concluir();
            
            //Assert
            Assert.True(tarefa.EstáConcluída());
        }
        
        [Test]
        public void ConcluirDeveSerIdempotente()
        {
            //Arrange
            var tarefa = new Tarefa("Finalizada");

            //Act
            tarefa.Concluir();
            tarefa.Concluir();
            
            //Assert
            Assert.True(tarefa.EstáConcluída());
        }
    }
}