using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Genero;
using VinylStore.Application.Dtos.Pais;
using VinylStore.Entities;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly ILogger<GenerosController> _logger;
        private readonly IApplication<Genero> _genero;
        private readonly IMapper _mapper;

        public GenerosController(
            ILogger<GenerosController> logger,
            UserManager<User> userManager,
            IApplication<Genero> genero,
            IMapper mapper)
        {
            _logger = logger;
            _genero = genero;
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
                return Ok(_mapper.Map<IList<GeneroResponseDto>>(_genero.GetAll()));
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
            Genero genero = _genero.GetById(Id.Value);
            if (genero is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GeneroResponseDto>(genero));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(GeneroRequestDto generoRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var genero = _mapper.Map<Genero>(generoRequestDto);
            _genero.Save(genero);
            return Ok(genero.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Genero generoBack = _genero.GetById(Id.Value);
            if (generoBack is null)
            { return NotFound(); }
            _genero.Delete(generoBack.Id);
            return Ok();
        }
    }
}
