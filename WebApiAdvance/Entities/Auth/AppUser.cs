using Microsoft.AspNetCore.Identity;

namespace WebApiAdvance.Entities.Auth
{
    public class AppUser<Guid>:IdentityUser
    {
        public string FullName { get; set; }
    }
}
