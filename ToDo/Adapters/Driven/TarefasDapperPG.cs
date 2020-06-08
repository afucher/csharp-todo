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
            var tarefaNoBanco = new Tarefa((uint)PróximoId(), tarefa.Título, tarefa.EstáConcluída());
            
            _conexão.Execute(@"insert into public.tarefas(id, titulo, concluida) values (@Id, @Título, @Concluída)",
                new[] { new {Id = (int)tarefaNoBanco.Id, tarefaNoBanco.Título, Concluída=tarefa.EstáConcluída() }}
            );
            return tarefaNoBanco;
        }

        private int PróximoId()
        {
            var últimoId = _conexão.ExecuteScalar(@"SELECT MAX(id) FROM public.tarefas");
            return (int?) últimoId + 1 ?? 1;
        }

        public void ExcluirTarefa(uint id)
        {
            _conexão.Execute(@"DELETE FROM public.tarefas WHERE id = @Id", new {Id = (int)id});
        }

        public void ConcluirTarefa(uint id)
        {
            _conexão.Execute(@"UPDATE public.tarefas SET concluida = true WHERE id = @Id", new {Id = (int)id});
        }
    }
}