using System.Collections.Generic;
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
            => await Context.Albums.ToListAsync();
    }
}