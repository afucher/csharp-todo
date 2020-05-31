using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public interface IExportador
    {
        void Exportar(IReadOnlyCollection<Tarefa> tarefas);
    }
}