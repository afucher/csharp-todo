using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Adapters
{
    [Table("tarefas")]
    public class TarefaDB
    {
        [Key] public int id { get; set; }
        public string titulo { get; set; }
        public bool concluida { get; set; }
    }
}