using NUnit.Framework;
using ToDo.Models;

namespace ToDoUnitTest
{
    public class TarefaTestes
    {
        [Test]
        public void ConstrutorDaTarefaDeveReceberValoresIniciais()
        {
            var tarefa = new Tarefa("Descobrir pq Limited WIP não funciona");

            Assert.AreEqual("Descobrir pq Limited WIP não funciona", tarefa.Título);
        }

        [Test]
        public void TarefaDeveSerNãoConcluídaQuandoCriada()
        {
            var tarefa = new Tarefa("Qualquer");
            
            Assert.False(tarefa.EstáConcluída());
        }
    }
}