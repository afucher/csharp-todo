using NUnit.Framework;
using ToDo.Services;

namespace ToDoUnitTest.Services
{
    public class ServiçoTarefaTeste
    {
        [Test]
        public void DeveTrazerNenhumaTarefa()
        {
            //Arrange
            var serviço = new ServiçoTarefa();

            //Act
            var tarefas = serviço.ObterTarefas();
            
            //Assert
            Assert.IsEmpty(tarefas);
        }
    }
}