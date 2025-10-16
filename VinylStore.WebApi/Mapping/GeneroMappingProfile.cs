using AutoMapper;
using VinylStore.Application.Dtos.Discografica;
using VinylStore.Application.Dtos.Genero;
using VinylStore.Entities;

namespace VinylStore.WebApi.Mapping
{
    public class GeneroMappingProfile: Profile
    {
        public GeneroMappingProfile()
        {
            CreateMap<Genero, GeneroResponseDto>();
            CreateMap<GeneroRequestDto, Genero>();
        }
    }
}
