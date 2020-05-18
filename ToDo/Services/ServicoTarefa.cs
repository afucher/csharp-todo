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
        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            return _fonteDadosTarefas.ObterTarefas();
        }
    }
}