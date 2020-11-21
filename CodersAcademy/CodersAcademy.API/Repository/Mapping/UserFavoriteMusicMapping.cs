using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodersAcademy.API.Repository.Mapping
{
    public class UserFavoriteMusicMapping : IEntityTypeConfiguration<UserFavoriteMusic>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteMusic> builder)
        {
            builder.ToTable("UserFavoriteMusic");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.MusicId);

            builder.HasOne(x => x.Music)
                .WithOne() // WithOne() está vazio pois a música não precisa saber se é favorita ou não
                .HasForeignKey<UserFavoriteMusic>(x => x.MusicId);
        }
    }
}