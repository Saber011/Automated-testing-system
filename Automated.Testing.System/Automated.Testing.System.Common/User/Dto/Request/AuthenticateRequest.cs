using System.ComponentModel.DataAnnotations;

namespace Automated.Testing.System.Common.User.Dto.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}