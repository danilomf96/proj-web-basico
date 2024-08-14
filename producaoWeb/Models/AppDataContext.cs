using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }

        // Construtor que aceita DbContextOptions
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        // Se desejar usar um construtor sem parâmetros para outros casos
        public AppDataContext()
        {
        }

        // O método OnConfiguring pode ser removido se a configuração for feita em Program.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=meubanco.db");
            }
        }
    }
}
