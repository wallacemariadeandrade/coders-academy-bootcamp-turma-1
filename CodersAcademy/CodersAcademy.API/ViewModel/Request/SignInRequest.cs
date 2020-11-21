using System.ComponentModel.DataAnnotations;

namespace CodersAcademy.API.ViewModel.Request
{
    public class SignInRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}