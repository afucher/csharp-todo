using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public interface IFonteDadosTarefas
    {
        IReadOnlyCollection<Tarefa> ObterTarefas();
        Tarefa CriarTarefa(Tarefa tarefa);
        void ExcluirTarefa(uint id);
    }
}