using AutoMapper;
using WebApiAdvance.Entities.Auth;
using WebApiAdvance.Entities.DTOs;

namespace WebApiAdvance.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterDto, AppUser<Guid>>();
        }
    }
}
