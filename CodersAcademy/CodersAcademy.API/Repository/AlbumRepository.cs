using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;

namespace CodersAcademy.API.Repository
{
    public class AlbumRepository
    {
        private MusicContext Context { get; init; }

        public AlbumRepository(MusicContext context)
        {
            Context = context;
        }

        public async Task<IList<Album>> GetAllAsync()
            => await Context.Albums.Include(x => x.Musics).ToListAsync();

        public async Task<Album> GetAlbumByIdAsync(Guid id)
            => await Context
                .Albums
                .Include(x => x.Musics)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        public async Task DeleteAsync(Album album)
        {
            Context.Remove(album);
            await Context.SaveChangesAsync(); // Realiza o commit da alteração
        }

        public async Task CreateAsync(Album album)
        {
            await Context.AddAsync(album);
            await Context.SaveChangesAsync();
        }

        public async Task<Music> GetMusicAsync(Guid musicId)
            => await Context
                    .Musics
                    .Where(x => x.Id == musicId)
                    .FirstOrDefaultAsync();
    }
}