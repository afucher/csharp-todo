using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Services;

namespace ToDo
{
    public class ConsoleProgram
    {
        public static void Executa()
        {
            // var fonteDeDados = new TarefasEmMemória();
            // var fonteDeDados = new TarefasDapperPG(new NpgsqlConnection(parametrosConexão));
            var fonteDeDados = new TarefasEFCorePG(new TarefasDbContext(new DbContextOptionsBuilder().UseNpgsql(Program.parametrosConexão).Options));
            var serviçoTarefa = new ServiçoTarefa(fonteDeDados);
            var serviçoExportar = new ServiçoExportar(fonteDeDados);
            
            var console = new ConsoleUI(serviçoTarefa, serviçoExportar);
            
            console.MostrarTarefas();
            // console.CriarTarefa();
            // console.MostrarTarefas();
            // console.ExcluirTarefa();
            // console.CriarTarefa();
            // console.MostrarTarefas();
            console.ConcluirTarefa();
            // console.MostrarTarefas();
            console.ExportarTarefas();
        }
    }
}