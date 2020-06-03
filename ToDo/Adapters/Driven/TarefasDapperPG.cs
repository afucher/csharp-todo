using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters
{
    public class TarefasDapperPG : IFonteDadosTarefas
    {
        private readonly IDbConnection _conexão;

        public TarefasDapperPG(IDbConnection conexão)
        {
            _conexão = conexão;
        }
        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            var tarefas = _conexão.Query<Tarefa>("select id as Id, titulo as Título, concluida as Concluída from public.tarefas");
            return tarefas.ToArray();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            return tarefa;
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