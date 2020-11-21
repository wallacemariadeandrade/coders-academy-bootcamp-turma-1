using System;
using System.Collections.Generic;

namespace CodersAcademy.API.ViewModel.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public List<FavoriteMusicResponse> FavoriteMusics { get; set; }
    }
}