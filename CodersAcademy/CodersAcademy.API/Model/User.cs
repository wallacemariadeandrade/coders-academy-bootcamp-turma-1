using System;
using System.Collections.Generic;
using System.Linq;

namespace CodersAcademy.API.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public IList<UserFavoriteMusic> FavoriteMusics { get; set; }

        public void AddFavoriteMusic(Music music) => 
            FavoriteMusics.Add(new UserFavoriteMusic{
                Music = music,
                MusicId = music.Id,
                User = this,
                UserId = this.Id
            });
        
        public void RemoveFavoriteMusic(Music music)
        {
            var favMusic = FavoriteMusics
                            .FirstOrDefault(x => x.MusicId == music.Id);
            
            if(favMusic == null)
                throw new ArgumentNullException("Music not found on the favorite list.");

            FavoriteMusics.Remove(favMusic);
        }
    }
}