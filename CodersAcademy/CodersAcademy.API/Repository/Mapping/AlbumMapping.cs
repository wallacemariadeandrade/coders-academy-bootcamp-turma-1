using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;

namespace CodersAcademy.API.Repository.Mapping
{
    // Mapeando os objetos para o banco de dados
    public class AlbumMapping : IEntityTypeConfiguration<Album>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albuns");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Band).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Backdrop).IsRequired(); // Nao possui tamanho maximo pois aqui sera colocada uma url

            // Ao deletar um album todas as músicas do mesmo serão deletadas
            builder.HasMany(x => x.Musics).WithOne(x => x.Album).OnDelete(DeleteBehavior.Cascade);
        }
    }
}