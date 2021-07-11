using System.ComponentModel.DataAnnotations;

namespace Hymn.Infra.CrossCutting.Identity.Models
{
    public class LoginUser
    {
        [Required]
        public string Identifier { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}