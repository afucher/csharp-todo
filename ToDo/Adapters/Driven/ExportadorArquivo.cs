using System.Collections.Generic;
using System.IO;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters
{
    public class ExportadorArquivo : IExportador
    {
        private readonly StreamWriter _arquivo;

        public ExportadorArquivo(StreamWriter arquivo)
        {
            _arquivo = arquivo;
        }
        public void Exportar(IReadOnlyCollection<Tarefa> tarefas)
        {
            foreach (var tarefa in tarefas)
            {
                _arquivo.WriteLine($"{tarefa.Id}##__##{tarefa.Título}##__##{tarefa.EstáConcluída()}");
            }
        }
    }
}