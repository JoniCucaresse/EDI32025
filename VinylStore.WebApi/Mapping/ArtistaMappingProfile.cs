using AutoMapper;
using System;
using VinylStore.Application.Dtos.Artista;
using VinylStore.Entities;

namespace VinylStore.WebApi.Mapping
{
    public class ArtistaMappingProfile: Profile
    {
        public ArtistaMappingProfile()
        {
            CreateMap<Artista, ArtistaResponseDto>();
            CreateMap<ArtistaRequestDto, Artista>();
        }
    }
}
