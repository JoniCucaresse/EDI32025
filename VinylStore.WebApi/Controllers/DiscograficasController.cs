using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Discografica;
using VinylStore.Entities;

namespace VinylStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscograficasController : ControllerBase
    {
        private readonly ILogger<DiscograficasController> _logger;
        private readonly IApplication<Discografica> _discografica;
        private readonly IMapper _mapper;

        public DiscograficasController(
            ILogger<DiscograficasController> logger,
            IApplication<Discografica> discografica,
            IMapper mapper)
        {
            _logger = logger;
            _discografica = discografica;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var items = _discografica.GetAll();
            return Ok(_mapper.Map<IList<DiscograficaResponseDto>>(items));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            var entity = _discografica.GetById(Id.Value);
            if (entity is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DiscograficaResponseDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DiscograficaRequestDto discograficaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<Discografica>(discograficaRequestDto);
            _discografica.Save(entity);
            return Ok(entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, DiscograficaRequestDto discograficaRequestDto)
        {
            if (!Id.HasValue || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existing = _discografica.GetById(Id.Value);
            if (existing is null)
            {
                return NotFound();
            }

            existing = _mapper.Map<Discografica>(discograficaRequestDto);
            _discografica.Save(existing);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            var existing = _discografica.GetById(Id.Value);
            if (existing is null)
            {
                return NotFound();
            }

            _discografica.Delete(existing.Id);
            return Ok();
        }
    }
}
