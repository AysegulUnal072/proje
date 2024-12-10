using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace proje.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public int KullanıcıId { get; set; }

        public string? Adres {  get; set; }

        public string? Fakulte { get; set; }


        public string? bolum { get; set; }

    }
}
