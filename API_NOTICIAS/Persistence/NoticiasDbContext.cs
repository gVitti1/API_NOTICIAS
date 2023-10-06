using API_NOTICIAS.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NOTICIAS.Persistence
{
    public class NoticiasDbContext : DbContext
    {
        //Classe de configuração do DbContext e preparação para as Migrações.
        public NoticiasDbContext(DbContextOptions<NoticiasDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }

    }
}
