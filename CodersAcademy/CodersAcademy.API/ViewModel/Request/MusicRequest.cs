using System.ComponentModel.DataAnnotations;

namespace CodersAcademy.API.ViewModel.Request
{
    public class MusicRequest
    {    
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}