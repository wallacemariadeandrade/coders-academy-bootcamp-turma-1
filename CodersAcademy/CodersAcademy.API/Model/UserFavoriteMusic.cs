using System;
using System.Text.Json.Serialization;

namespace CodersAcademy.API.Model
{
    /*
        Este modelo tem como finalidade a representação]
        do relacionamento entre o usuário e suas 
        músicas favoritas.
    */
    public class UserFavoriteMusic
    {
        public Guid Id { get; set; }
        public Guid MusicId { get; set; }
        public Guid UserId { get; set; }
        public Music Music { get; set; }
        [JsonIgnore] // Para evitar problemas de loop no EF
        public User User { get; set; }
    }
}