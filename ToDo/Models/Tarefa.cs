namespace ToDo.Models
{
    public class Tarefa
    {
        public string Título { get; }

        public Tarefa(string título)
        {
            Título = título;
        }
    }
}