using System;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Services;

namespace ToDo
{
    class Program
    {
        static void Main(string[] args)
        {
            var fonteDeDados = new TarefasEmMemória();
            var serviçoTarefa = new ServiçoTarefa(fonteDeDados);
            var serviçoExportar = new ServiçoExportar(fonteDeDados);
            
            var console = new ConsoleUI(serviçoTarefa, serviçoExportar);
            
            console.MostrarTarefas();
            console.CriarTarefa();
            console.MostrarTarefas();
            console.CriarTarefa();
            console.MostrarTarefas();
            console.ExportarTarefas();

        }
    }
}