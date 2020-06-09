using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Adapters
{
    public class TarefasDbContext : DbContext
    {
        public DbSet<TarefaDB> Tarefas { get; set; }
        public TarefasDbContext(DbContextOptions opções) : base(opções)
        {
            
        }
    }
}