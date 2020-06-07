using System;
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
            return _conexão
                    .Query("select id, titulo, concluida from public.tarefas")
                    .Select(tarefa => new Tarefa(Convert.ToUInt32(tarefa.id), tarefa.titulo, tarefa.concluida))
                    .ToArray();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            var tarefaNoBanco = new Tarefa(1, tarefa.Título, tarefa.EstáConcluída());
            var count = _conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[] { new {Id = (int)tarefaNoBanco.Id, tarefaNoBanco.Título, Concluída=tarefa.EstáConcluída() }}
            );
            return tarefaNoBanco;
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