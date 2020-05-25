using System.Linq;
using ToDo.Services;
using static System.Console;

namespace ToDo.Adapters.Driving
{
    public class ConsoleUI
    {
        private readonly ServiçoTarefa _serviçoTarefa;

        public ConsoleUI(ServiçoTarefa serviçoTarefa)
        {
            _serviçoTarefa = serviçoTarefa;
        }

        public void MostrarTarefas()
        {
            var tarefas = _serviçoTarefa.ObterTarefas();

            if (tarefas.Any())
            {
                foreach (var tarefa in tarefas)
                {
                    WriteLine($"[{tarefa.Id}] - {tarefa.Título}");
                }

            }
            else
            { 
                WriteLine("Nenhuma tarefa");
            }
            
        }
    }
}