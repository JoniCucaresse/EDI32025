using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Artista;
using VinylStore.Entities;

namespace VinylStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        private readonly ILogger<ArtistasController> _logger;
        private readonly IApplication<Artista> _artista;
        private readonly IMapper _mapper;
        public ArtistasController(ILogger<ArtistasController> logger, IApplication<Artista> artista, IMapper mapper)
        {
            _logger = logger;
            _artista = artista;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<ArtistaResponseDto>>(_artista.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Artista artista = _artista.GetById(Id.Value);
            if (artista is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ArtistaResponseDto>(artista));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ArtistaRequestDto artistaRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var artista = _mapper.Map<Artista>(artistaRequestDto);
            _artista.Save(artista);
            return Ok(artista.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, ArtistaRequestDto artistaRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest("ID requerido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Artista artistaBack = _artista.GetById(Id.Value);

            if (artistaBack is null)
            {
                return NotFound($"No se encontró el artista con ID {Id.Value}");
            }

            _mapper.Map(artistaRequestDto, artistaBack);

            artistaBack.Id = Id.Value;

            _artista.Save(artistaBack);

            var artistaResponseDto = _mapper.Map<ArtistaResponseDto>(artistaBack);

            return Ok(artistaResponseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Artista artistaBack = _artista.GetById(Id.Value);
            if (artistaBack is null)
            { return NotFound(); }
            _artista.Delete(artistaBack.Id);
            return Ok();
        }
    }
}
