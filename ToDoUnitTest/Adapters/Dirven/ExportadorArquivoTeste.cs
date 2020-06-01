using System.IO;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ToDo.Adapters;
using ToDo.Models;

namespace ToDoUnitTest.Adapters
{
    public class ExportadorArquivoTeste
    {
        [Test]
        public void Exportar_NãoDeveEscreverNoArquivo_QuandoNãoHouverTarefas()
        {
            var arquivo = Substitute.For<StreamWriter>(@".\teste.txt");
            var exportador = new ExportadorArquivo(arquivo);
            
            exportador.Exportar(new Tarefa[]{});

            arquivo.ReceivedCalls().Should().BeEmpty();
        }

        [Test]
        public void Exportar_DeveEscreverTarefaNoArquivo()
        {
            var arquivo = Substitute.For<StreamWriter>("./teste.txt");
            var exportador = new ExportadorArquivo(arquivo);
            
            exportador.Exportar(new []{new Tarefa(1, "Minha tarefa")});
            
            arquivo.Received().WriteLine("1##__##Minha tarefa##__##False");
        }
        
        [Test]
        public void Exportar_DeveEscreverMultiplasTarefasNoArquivo()
        {
            var arquivo = Substitute.For<StreamWriter>("./teste.txt");
            var exportador = new ExportadorArquivo(arquivo);
            var tarefaConcluída = new Tarefa(2, "Minha segunda tarefa");
            tarefaConcluída.Concluir();
            
            exportador.Exportar(new []
            {
                new Tarefa(1, "Minha tarefa"),
                tarefaConcluída,
                new Tarefa(3, "Minha terceira tarefa")
            });
            
            Received.InOrder(() =>
            {
                arquivo.WriteLine("1##__##Minha tarefa##__##False");
                arquivo.WriteLine("2##__##Minha segunda tarefa##__##True");
                arquivo.WriteLine("3##__##Minha terceira tarefa##__##False");
            });
        }
    }
}