using System;
using Npgsql;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Services;

namespace ToDo
{
    class Program
    {
        private static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=todo";
        static void Main(string[] args)
        {
            
            // var fonteDeDados = new TarefasEmMemória();
            var fonteDeDados = new TarefasDapperPG(new NpgsqlConnection(parametrosConexão));
            var serviçoTarefa = new ServiçoTarefa(fonteDeDados);
            var serviçoExportar = new ServiçoExportar(fonteDeDados);
            
            var console = new ConsoleUI(serviçoTarefa, serviçoExportar);
            
            console.MostrarTarefas();
            // console.CriarTarefa();
            // console.MostrarTarefas();
            // console.CriarTarefa();
            // console.MostrarTarefas();
            // console.ConcluirTarefa();
            // console.ExportarTarefas();

        }
    }
}