using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Discografica;
using VinylStore.Application.Dtos.Pais;
using VinylStore.Entities;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PaisesController> _logger;
        private readonly IApplication<Pais> _pais;
        private readonly IMapper _mapper;

        public PaisesController(ILogger<PaisesController> logger, 
            UserManager<User> userManager,
            IApplication<Pais> pais, 
            IMapper mapper)
        {
            _logger = logger;
            _pais = pais;
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
                return Ok(_mapper.Map<IList<PaisResponseDto>>(_pais.GetAll()));
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
            Pais pais = _pais.GetById(Id.Value);
            if (pais is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PaisResponseDto>(pais));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var pais = _mapper.Map<Pais>(paisRequestDto);
            _pais.Save(pais);
            return Ok(pais.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, PaisRequestDto paisRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest("ID requerido");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pais paisBack = _pais.GetById(Id.Value);

            if (paisBack is null)
            {
                return NotFound($"No se encontró el pais con ID {Id.Value}");
            }

            _mapper.Map(paisRequestDto, paisBack);

            paisBack.Id = Id.Value;

            _pais.Save(paisBack);

            var paisResponseDto = _mapper.Map<PaisResponseDto>(paisBack);

            return Ok(paisResponseDto);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Pais paisBack = _pais.GetById(Id.Value);
            if (paisBack is null)
            { return NotFound(); }
            _pais.Delete(paisBack.Id);
            return Ok();
        }
    }
}
