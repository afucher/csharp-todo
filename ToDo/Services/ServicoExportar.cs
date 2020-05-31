using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Services
{
    public class ServiçoExportar
    {
        private readonly IFonteDadosTarefas _fonteDeDados;
        private readonly IExportador _exportador;

        public ServiçoExportar(IFonteDadosTarefas fonteDeDados, IExportador exportador)
        {
            _fonteDeDados = fonteDeDados;
            _exportador = exportador;
        }

        public void Exportar()
        {
            _exportador.Exportar(_fonteDeDados.ObterTarefas());
        }
    }
}