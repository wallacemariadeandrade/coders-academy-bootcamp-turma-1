using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory Logger = LoggerFactory.Create(x => x.AddConsole()); // Para log das queries executadas pelo EF
            optionsBuilder.UseLoggerFactory(Logger); // Precisa do pacote Microsoft.Extensions.Logging e Microsoft.Extensions.Logging.Console (para log no console)
            base.OnConfiguring(optionsBuilder);
        }
    }
}