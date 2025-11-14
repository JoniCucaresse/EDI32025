using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Artista;
using VinylStore.Entities;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ArtistasController> _logger;
        private readonly IApplication<Artista> _artista;
        private readonly IMapper _mapper;
        public ArtistasController(ILogger<ArtistasController> logger, 
            UserManager<User> userManager, 
            IApplication<Artista> artista, 
            IMapper mapper)
        {
            _logger = logger;
            _artista = artista;
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
                    return Ok(_mapper.Map<IList<ArtistaResponseDto>>(_artista.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los artistas");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los artistas");
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
                Artista artista = _artista.GetById(Id.Value);
                if (artista is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ArtistaResponseDto>(artista));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el artista con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el artista");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(ArtistaRequestDto artistaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                { return BadRequest(); }
                var artista = _mapper.Map<Artista>(artistaRequestDto);
                _artista.Save(artista);
                return Ok(artista.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo artista");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el artista");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? Id, ArtistaRequestDto artistaRequestDto)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el artista con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al editar el artista");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el artista con ID {Id}", Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el artista");
            }
        }
    }
}
