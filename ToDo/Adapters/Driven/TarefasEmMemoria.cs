using System.Collections.Generic;
using System.Linq;
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
            var id = (uint)_tarefas.Count+1;
            var tarefa = new Tarefa(id, tarefaParaCriar.Título);
            _tarefas.Add(tarefa);
            return new Tarefa(id, tarefa.Título);
        }

        public void ExcluirTarefa(uint id)
        {
            _tarefas.RemoveAll(tarefa => tarefa.Id.Equals(id));
        }
    }
}