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
            var últimoId = _contexto.Tarefas.Max(tarefa => (int?)tarefa.id) ?? 0;
            var próximoId = últimoId + 1;
            _contexto.Tarefas.Add(new TarefaDB{id = próximoId, titulo = tarefa.Título, concluida = tarefa.EstáConcluída()});
            _contexto.SaveChanges();
            return new Tarefa((uint)próximoId, tarefa.Título, tarefa.EstáConcluída());
        }

        public void ExcluirTarefa(uint id)
        {
            var tarefa = _contexto.Tarefas.FirstOrDefault(tarefa => tarefa.id == (int)id);
            if(tarefa == null) return;
            
            _contexto.Tarefas.Remove(tarefa);
            _contexto.SaveChanges();
        }

        public void ConcluirTarefa(uint id)
        {
            var tarefa = _contexto.Tarefas.SingleOrDefault(tarefa => tarefa.id == (int)id);
            if(tarefa == null) return;

            tarefa.concluida = true;
            _contexto.SaveChanges();
        }
    }
}