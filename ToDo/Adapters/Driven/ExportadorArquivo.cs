using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (!tarefas.Any()) return;
            
            foreach (var tarefa in tarefas)
            {
                _arquivo.WriteLine($"{tarefa.Id}##__##{tarefa.Título}##__##{tarefa.EstáConcluída()}");
            }
            _arquivo.Flush();
            _arquivo.Close();
        }
    }
}