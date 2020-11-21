using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using CodersAcademy.API.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodersAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository UserRepository { get; }
        private IMapper Mapper { get; }
        private AlbumRepository AlbumRepository { get; }

        public UserController(UserRepository userRepository, IMapper mapper, AlbumRepository albumRepository)
        {
            UserRepository = userRepository;
            Mapper = mapper;
            AlbumRepository = albumRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await UserRepository.GetUserAsync(id);
            if(user == null) return NotFound();
            
            var result = Mapper.Map<UserResponse>(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await UserRepository.GetAllAsync();

            var result = Mapper.Map<List<UserResponse>>(users);

            return Ok(result);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var passwordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));
            var user = await UserRepository.AuthenticateAsync(request.Email, passwordBase64);

            if(user == null) return Unauthorized(new {
                Message = "Invalid Email/Password"
            });

            var result = Mapper.Map<UserResponse>(user);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = Mapper.Map<User>(request);

            user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password)); // encriptação simplória
            user.Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any";
            
            await UserRepository.CreateAsync(user);

            var result = Mapper.Map<UserResponse>(user);
            return  Created($"{result.Id}", result);
        }

        [HttpPost("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> SaveUserFavoriteMusic(Guid id, Guid musicId)
        {
            var music = await AlbumRepository.GetMusicAsync(musicId);

            var user = await UserRepository.GetUserAsync(id);

            if(user == null) return UnprocessableEntity(new { Message = "User not found." });
            if(music == null) return UnprocessableEntity(new { Message = "Music not found." });

            user.AddFavoriteMusic(music);

            await UserRepository.UpdateAsync(user);

            return Ok();
        }

        [HttpDelete("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> RemoveUserFavoriteMusic(Guid id, Guid musicId)
        {
            var music = await AlbumRepository.GetMusicAsync(musicId);

            var user = await UserRepository.GetUserAsync(id);

            if(user == null) return UnprocessableEntity(new { Message = "User not found." });
            if(music == null) return UnprocessableEntity(new { Message = "Music not found." });

            user.RemoveFavoriteMusic(music);

            await UserRepository.UpdateAsync(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var user = await UserRepository.GetUserAsync(id);
            if(user == null) return UnprocessableEntity(new { Message = "User not found." });
            await UserRepository.RemoveAsync(user);
            return NoContent();
        }
    }
}