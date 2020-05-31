using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public class ServiçoTarefa
    {
        private readonly IFonteDadosTarefas _fonteDadosTarefas;

        public ServiçoTarefa(IFonteDadosTarefas fonteDadosTarefas)
        {
            _fonteDadosTarefas = fonteDadosTarefas;
        }
        public virtual IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            return _fonteDadosTarefas.ObterTarefas();
        }

        public virtual Tarefa CriaTarefa(string título)
        {
            var tarefa = new Tarefa(título);

            return _fonteDadosTarefas.CriarTarefa(tarefa);
        }

        public void ExcluirTarefa(uint id)
        {
            _fonteDadosTarefas.ExcluirTarefa(id);
        }
    }
}