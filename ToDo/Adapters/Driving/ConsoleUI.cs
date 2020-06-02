using System;
using System.IO;
using System.Linq;
using ToDo.Exceptions;
using ToDo.Services;
using static System.Console;

namespace ToDo.Adapters.Driving
{
    public class ConsoleUI
    {
        private readonly ServiçoTarefa _serviçoTarefa;
        private readonly ServiçoExportar _serviçoExportar;

        public ConsoleUI(ServiçoTarefa serviçoTarefa, ServiçoExportar serviçoExportar)
        {
            _serviçoTarefa = serviçoTarefa;
            _serviçoExportar = serviçoExportar;
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

        public void ExportarTarefas()
        {
            Write("Arquivo destino: ");
            var arquivo = ReadLine();
            _serviçoExportar.Exportar(new ExportadorArquivo(new StreamWriter(arquivo)));
        }

        public void ConcluirTarefa()
        {
            Write("Tarefa a ser concluída: ");
            var id = ReadLine();
            _serviçoTarefa.ConcluirTarefa(Convert.ToUInt32(id));
        }
    }
}