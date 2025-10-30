using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Genero;
using VinylStore.Entities;

namespace VinylStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
    
        private readonly ILogger<GenerosController> _logger;
        private readonly IApplication<Genero> _genero;
        private readonly IMapper _mapper;

        public GenerosController(
            ILogger<GenerosController> logger,
            IApplication<Genero> genero,
            IMapper mapper)
        {
            _logger = logger;
            _genero = genero;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var items = _genero.GetAll();
            return Ok(_mapper.Map<IList<GeneroResponseDto>>(items));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            var entity = _genero.GetById(Id.Value);
            if (entity is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GeneroResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(GeneroRequestDto generoRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<Genero>(generoRequestDto);
            _genero.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, GeneroRequestDto generoRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest("ID requerido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genero generoBack = _genero.GetById(Id.Value);

            if (generoBack is null)
            {
                return NotFound($"No se encontró el genero con ID {Id.Value}");
            }

            _mapper.Map(generoRequestDto, generoBack);

            generoBack.Id = Id.Value;

            _genero.Save(generoBack);

            var generoResponseDto = _mapper.Map<GeneroResponseDto>(generoBack);

            return Ok(generoResponseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            var existing = _genero.GetById(Id.Value);
            if (existing is null)
            {
                return NotFound();
            }

            _genero.Delete(existing.Id);
            return Ok();
        }
    }
}
