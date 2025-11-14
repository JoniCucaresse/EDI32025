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
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los países");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los países");
            }
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ById(int? Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el país con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el país");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                { 
                    return BadRequest(); 
                }
                var pais = _mapper.Map<Pais>(paisRequestDto);
                _pais.Save(pais);
                return Ok(pais.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo país");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el país");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, PaisRequestDto paisRequestDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el país con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al editar el país");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            try
            {
                if (!Id.HasValue)
                { 
                    return BadRequest(); 
                }
                if (!ModelState.IsValid)
                { 
                    return BadRequest(); 
                }
                Pais paisBack = _pais.GetById(Id.Value);
                if (paisBack is null)
                { 
                    return NotFound(); 
                }
                _pais.Delete(paisBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el país con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el país");
            }
        }
    }
}
