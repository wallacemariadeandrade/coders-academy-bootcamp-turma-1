using System;
using System.Collections.Generic;

namespace CodersAcademy.API.Model
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Backdrop { get; set; }
        public string Band { get; set; }
        public IList<Music> Musics { get; set; }
    }
}