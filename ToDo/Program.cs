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
            var serviçoTarefa = new ServiçoTarefa(new TarefasEmMemória());
            
            var console = new ConsoleUI(serviçoTarefa);
            
            console.MostrarTarefas();
            console.CriarTarefa();
            console.MostrarTarefas();

        }
    }
}