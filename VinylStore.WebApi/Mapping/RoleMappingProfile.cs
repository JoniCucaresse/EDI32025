using AutoMapper;
using VinylStore.Application.Dtos.Identity.Roles;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.WebApi.Mapping
{
    public class RoleMappingProfile: Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
