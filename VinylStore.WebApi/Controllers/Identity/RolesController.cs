using AutoMapper;
using VinylStore.Application.Dtos.Identity.Roles;
using VinylStore.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace VinylStore.WebApi.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<Role> roleManager
            , ILogger<RolesController> logger
            , IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IList<RoleResponseDto>>(_roleManager.Roles.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener roles");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los roles");
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Guardar(RoleRequestDto roleRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos enviados no son validos.");
            }

            try
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                var role = _mapper.Map<Role>(roleRequestDto);
                role.Id = Guid.NewGuid();
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(role.Id);
                }
                return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el rol");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el rol");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Modificar([FromBody] RoleRequestDto roleRequestDto, [FromQuery] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos enviados no son validos.");
            }

            try
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                var role = _mapper.Map<Role>(roleRequestDto);
                role.Id = id;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(role.Id);
                }
                return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol con ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el rol");
            }
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                var role = await _roleManager.FindByIdAsync(id.Value.ToString());
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleResponseDto>(role));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol con ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el rol");
            }
        }
    }
}
