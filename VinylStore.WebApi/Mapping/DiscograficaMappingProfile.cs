using AutoMapper;
using VinylStore.Application.Dtos.Artista;
using VinylStore.Application.Dtos.Discografica;
using VinylStore.Entities;

namespace VinylStore.WebApi.Mapping
{
    public class DiscograficaMappingProfile:Profile
    {
        public DiscograficaMappingProfile()
        {
            CreateMap<Discografica, DiscograficaResponseDto>();
            CreateMap<DiscograficaRequestDto, Discografica>();
        }
    }
}
