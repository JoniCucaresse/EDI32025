using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Application.Dtos.Cliente;
using VinylStore.Entities;

namespace VinylStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IApplication<Cliente> _cliente;
        private readonly IMapper _mapper;
        public ClientesController(ILogger<ClientesController> logger, IApplication<Cliente> cliente, IMapper mapper)
        {
            _logger = logger;
            _cliente = cliente;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<ClienteResponseDto>>(_cliente.GetAll()));
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            Cliente cliente = _cliente.GetById(Id.Value);
            if (cliente is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClienteResponseDto>(cliente));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteRequestDto clienteRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var cliente = _mapper.Map<Cliente>(clienteRequestDto);
            _cliente.Save(cliente);
            return Ok(cliente.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, ClienteRequestDto clienteRequestDto)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Cliente clienteBack = _cliente.GetById(Id.Value);
            if (clienteBack is null)
            { return NotFound(); }
            clienteBack = _mapper.Map<Cliente>(clienteRequestDto);
            _cliente.Save(clienteBack);
            return Ok(clienteBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Cliente clienteBack = _cliente.GetById(Id.Value);
            if (clienteBack is null)
            { return NotFound(); }
            _cliente.Delete(clienteBack.Id);
            return Ok();
        }
    }
}
