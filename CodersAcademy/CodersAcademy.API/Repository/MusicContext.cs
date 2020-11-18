using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CodersAcademy.API.Repository
{
    // O DbContext faz a interface com o banco de dados
    public class MusicContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumMapping());
            modelBuilder.ApplyConfiguration(new MusicMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}