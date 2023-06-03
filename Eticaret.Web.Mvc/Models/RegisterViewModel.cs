using System.ComponentModel.DataAnnotations;

namespace Eticaret.Web.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string? Password2 { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
