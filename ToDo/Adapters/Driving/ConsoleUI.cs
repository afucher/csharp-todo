using System;
using System.Linq;
using ToDo.Exceptions;
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

        public void CriarTarefa()
        {
            Write("Qual o título da tarefa: ");
            var título = ReadLine();
            try
            {
                var tarefa = _serviçoTarefa.CriaTarefa(título);
                WriteLine($"Tarefa criada com Id: {tarefa.Id}"); 
            }
            catch (TítuloInválidoExceção e)
            {
                WriteLine("Título inválido para tarefa");
            }
        }

        public void ExcluirTarefa()
        {
            Write("Qual id da tarefa para excluir: ");
            var id = ReadLine();
            _serviçoTarefa.ExcluirTarefa(Convert.ToUInt32(id));
            WriteLine("Tarefa excluída.");
        }
    }
}