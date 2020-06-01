using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public class ServiçoExportar
    {
        private readonly IFonteDadosTarefas _fonteDeDados;

        public ServiçoExportar(IFonteDadosTarefas fonteDeDados)
        {
            _fonteDeDados = fonteDeDados;
        }

        public virtual void Exportar(IExportador exportador)
        {
            exportador.Exportar(_fonteDeDados.ObterTarefas());
        }
    }
}