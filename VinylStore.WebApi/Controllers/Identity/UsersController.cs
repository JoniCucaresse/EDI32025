using AutoMapper;
using VinylStore.Application.Dtos.Identity.Roles;
using VinylStore.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace VinylStore.WebApi.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;
        public UsersController(RoleManager<Role> roleManager
            , UserManager<User> userManage
            , ILogger<UsersController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManage;
        }
        
        [HttpPost]
        [Route("AddRoleToUser")]
        public async Task<IActionResult> Guardar(string userId, string roleId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var role = await _roleManager.FindByIdAsync(roleId);

                if (user is not null && role is not null)
                {
                    var status = await _userManager.AddToRoleAsync(user, role.Name);
                    if (status.Succeeded)
                    {
                        return Ok(new { user = user.UserName, rol = role.Name });
                    }

                    return Problem(detail: status.Errors.First().Description, statusCode: StatusCodes.Status409Conflict);
                }

                return BadRequest(new { userId = userId, roleId = roleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar el rol {RoleId} al usuario {UserId}", roleId, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al asignar el rol al usuario");
            }
        }
    }
}
