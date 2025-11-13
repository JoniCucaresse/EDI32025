using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Artista;
using VinylStore.Application.Dtos.Discografica;
using VinylStore.Entities;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscograficasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DiscograficasController> _logger;
        private readonly IApplication<Discografica> _discografica;
        private readonly IMapper _mapper;

        public DiscograficasController(
            ILogger<DiscograficasController> logger,
            UserManager<User> userManager,
            IApplication<Discografica> discografica,
            IMapper mapper)
        {
            _logger = logger;
            _discografica = discografica;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> All()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (_userManager.IsInRoleAsync(user, "Administrador").Result ||
                _userManager.IsInRoleAsync(user, "Cliente").Result)
            {
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<DiscograficaResponseDto>>(_discografica.GetAll()));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Discografica discografica = _discografica.GetById(Id.Value);
            if (discografica is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DiscograficaResponseDto>(discografica));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(DiscograficaRequestDto discograficaRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var discografica = _mapper.Map<Discografica>(discograficaRequestDto);
            _discografica.Save(discografica);
            return Ok(discografica.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, DiscograficaRequestDto discograficaRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest("ID requerido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Discografica discograficaBack = _discografica.GetById(Id.Value);

            if (discograficaBack is null)
            {
                return NotFound($"No se encontró la discografica con ID {Id.Value}");
            }

            _mapper.Map(discograficaRequestDto, discograficaBack);

            discograficaBack.Id = Id.Value;

            _discografica.Save(discograficaBack);

            var discograficaResponseDto = _mapper.Map<DiscograficaResponseDto>(discograficaBack);

            return Ok(discograficaResponseDto);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Discografica discograficaBack = _discografica.GetById(Id.Value);
            if (discograficaBack is null)
            { return NotFound(); }
            _discografica.Delete(discograficaBack.Id);
            return Ok();
        }
    }
}
