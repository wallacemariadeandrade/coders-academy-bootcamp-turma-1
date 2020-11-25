using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodersAcademy.API.ViewModel.Request
{
    public class AlbumRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Band { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Backdrop { get; set; }
        [Required]
        public List<MusicRequest> Musics { get; set; }

        // Para validação customizada
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if(Musics == null) 
                yield return new ValidationResult("Album must contain at least one music.");

            if(!Musics.Any()) 
                yield return new ValidationResult("Album must contain at least one music."); // Valida se ha uma musica pelo menos
            // Valida todas as propriedadaes do objeto musica
            foreach(var music in Musics)
            {
                // result armazena cada validação com falha
                if(!Validator.TryValidateObject(music, new ValidationContext(music), result))   
                    yield return result.First();
            }
        }
    }
}