using AutoMapper;
using VinylStore.Application.Dtos.Cliente;
using VinylStore.Entities;

namespace VinylStore.WebApi.Mapping
{
    public class ClienteMappingProfile: Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Cliente, ClienteResponseDto>().
                ForMember(dest => dest.FechaDeRegistro, ori => ori.MapFrom(src => src.FechaDeRegistro.ToShortDateString()));
            CreateMap<ClienteRequestDto, Cliente>();
        }
    }
}
