using NUnit.Framework;
using ToDo.Exceptions;

namespace ToDoUnitTest.Exceptions
{
    public class TituloInvalidoExcecaoTeste
    {
        [Test]
        public void DeveConterMensagemPadrão()
        {
            var exceção = new TítuloInválidoExceção();
            
            Assert.AreEqual( "Título deve ter valor.",exceção.Message);
        }
    }
}