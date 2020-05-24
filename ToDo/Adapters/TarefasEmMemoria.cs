using System.Collections.Generic;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters
{
    public class TarefasEmMemória : IFonteDadosTarefas
    {
        List<Tarefa> _tarefas = new List<Tarefa>();
        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            return _tarefas;
        }

        public Tarefa CriarTarefa(Tarefa tarefaParaCriar)
        {
            var tarefa = new Tarefa((uint)_tarefas.Count+1, tarefaParaCriar.Título);
            _tarefas.Add(tarefa);
            return tarefa;
        }
    }
}