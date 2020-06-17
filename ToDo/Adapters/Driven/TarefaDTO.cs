using ToDo.Models;

namespace ToDo.Adapters
{
    public class TarefaDTO
    {
        public uint id { get; set; }
        public string titulo { get; set; }
        public bool concluida { get; set; }

        public Tarefa Tarefa()
        {
            return new Tarefa(id, titulo, concluida);
        }
    }
}