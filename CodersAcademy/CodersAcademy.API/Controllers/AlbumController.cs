using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private AlbumRepository Repository { get; set; }
        private IMapper Mapper { get; set; }

        public AlbumController(AlbumRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbuns()
        {
            return Ok(await Repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(Guid id)
        {
            var result = await Repository.GetAlbumByIdAsync(id);

            // Somente C#9 
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status201Created)] // Created
        public async Task<IActionResult> SaveAlbuns(AlbumRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var album = Mapper.Map<Album>(request); // Profile configurado em ViewModel.Profile

            await Repository.CreateAsync(album);

            return Created($"/{album.Id}", album);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var result = await Repository.GetAlbumByIdAsync(id);
            if(result == null) return NotFound();
            await Repository.DeleteAsync(result);
            return NoContent(); // Ação realizada com sucesso, mas não há o que ser exibido
            /*
                Em caso de erro poderia ser retornado HTTP 500 ou 422 (erro de processamento)
            */
        }

    }
}
