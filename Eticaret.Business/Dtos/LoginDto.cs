using System.ComponentModel.DataAnnotations;

namespace Eticaret.Business.Dtos
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}