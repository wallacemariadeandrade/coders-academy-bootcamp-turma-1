using System;
using System.Text.Json.Serialization;

namespace CodersAcademy.API.Model
{
    public class Music
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        [JsonIgnore] // Para evitar loop na serialização (Album contém música e música contém album)
        public Album Album { get; set; }
    }
}