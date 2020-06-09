using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters
{
    public class TarefasEFCorePG : IFonteDadosTarefas
    {
        private readonly TarefasDbContext _contexto;

        public TarefasEFCorePG(TarefasDbContext contexto)
        {
            _contexto = contexto;
        }

        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            return _contexto
                .Tarefas
                .Select(tarefa => new Tarefa((uint)tarefa.id, tarefa.titulo, tarefa.concluida))
                .ToArray();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            throw new System.NotImplementedException();
        }

        public void ExcluirTarefa(uint id)
        {
            throw new System.NotImplementedException();
        }

        public void ConcluirTarefa(uint id)
        {
            throw new System.NotImplementedException();
        }
    }
}