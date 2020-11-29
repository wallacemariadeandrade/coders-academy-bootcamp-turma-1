using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodersAcademy.API
{
    public static class Seed
    {
        public static async Task InitDbAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<MusicContext>();
            await context.Database.MigrateAsync();
            
            if(!await context.Albums.AnyAsync())
            {
                context.Albums.AddRange 
                (
                    new Album
                    {
                        Name = "Abbey Road",
                        Band = "The Beatles",
                        Description =  "The Beatles foi uma banda de rock inglesa formada em 1960 na cidade de Liverpool. Formada por John Lennon, Paul McCartney, George Harrison e Ringo Starr, é considerada a banda mais influente de todos os tempos",
                        Backdrop = "https://www.hypeness.com.br/1/2018/03/Capas-11.jpg",
                        Musics = new List<Music> 
                        {
                            new Music
                            {
                                Name = "Come Together",
                                Duration = 260
                            },
                            new Music
                            {
                                Name = "Something",
                                Duration = 197
                            },
                            new Music
                            {
                                Name =  "Maxwell's Silver Hammer",
                                Duration = 196
                            },
                            new Music
                            {
                                Name = "Oh! Darling",
                                Duration = 196
                            },
                            new Music
                            {
                                Name = "I Want You (She's So Heavy)",
                                Duration = 467
                            },
                            new Music
                            {
                                Name = "Here Comes the Sun",
                                Duration = 185
                            },  
                            new Music
                            {
                                Name = "Because",
                                Duration = 165
                            }
                        }
                    }
                );
            }
        }
    }
}