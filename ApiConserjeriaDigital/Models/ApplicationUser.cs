using Microsoft.AspNetCore.Identity;

namespace ApiConserjeriaDigital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Rut {  get; set; }
    }
}
