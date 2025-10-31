using AutoMapper;
using VinylStore.Application.Dtos.Pais;
using VinylStore.Entities;

namespace VinylStore.WebApi.Mapping
{
    public class PaisMappingProfile: Profile
    {
        public PaisMappingProfile()
        {
            CreateMap<Pais, PaisResponseDto>();
            CreateMap<PaisRequestDto, Pais>();
        }
    }
}
