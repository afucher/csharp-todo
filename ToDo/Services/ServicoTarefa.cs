using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public class ServiçoTarefa
    {
        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            return new List<Tarefa>();
        }
    }
}