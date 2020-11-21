using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;

namespace CodersAcademy.API.Repository
{
    public class UserRepository
    {
        private MusicContext Context { get; set; }

        public UserRepository(MusicContext context)
        {
            Context = context;
        }

        public async Task CreateAsync(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateAsync(string email, string password)
            => await Context
                    .Users
                    .Include(x => x.FavoriteMusics) 
                    .ThenInclude(x => x.Music) // Carrega propriedade Music que esta dentro de FavoriteMusics
                    .ThenInclude(x => x.Album) // Carrega propriedade Album que esta dentro de Music
                    .Where(x => x.Password == password && x.Email == email)
                    .FirstOrDefaultAsync();

        public async Task<User> GetUserAsync(Guid id)
            => await Context
                    .Users
                    .Include(x => x.FavoriteMusics)
                    .ThenInclude(x => x.Music)
                    .ThenInclude(x => x.Album)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

        public async Task<IList<User>> GetAllAsync() 
            => await Context
                    .Users
                    .Include(x => x.FavoriteMusics)
                    .ThenInclude(x => x.Music)
                    .ThenInclude(x => x.Album)
                    .ToListAsync();

        public async Task UpdateAsync(User user)
        {
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            Context.Remove(user);
            await Context.SaveChangesAsync();
        }
    }
}