using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ToDo.Models;
using ToDo.Services;

namespace ToDoUnitTest.Services
{
    public class ServiçoExportarTeste
    {
        [Test]
        public void DeveExportarTarefas()
        {
            var exportador = Substitute.For<IExportador>();
            var fonteDeDados = Substitute.For<IFonteDadosTarefas>();
            var tarefas = new[] {new Tarefa(1, "Tarefa 1")};
            var serviçoExportar = new ServiçoExportar(fonteDeDados, exportador);
            fonteDeDados.ObterTarefas().Returns(tarefas);

            serviçoExportar.Exportar();

            exportador.Received().Exportar(tarefas);
        }
    }
}