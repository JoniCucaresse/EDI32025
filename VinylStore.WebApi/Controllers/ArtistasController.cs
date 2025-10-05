using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Application;
using VinylStore.Entities;

namespace VinylStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        private readonly ILogger<ArtistasController> _logger;
        private readonly IApplication<Artista> _artista;
        public ArtistasController(ILogger<ArtistasController> logger, IApplication<Artista> artista)
        {
            _logger = logger;
            _artista = artista;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_artista.GetAll());
        }

        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
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
            return Ok(artista);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Artista artista)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            _artista.Save(artista);
            return Ok(artista.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, Artista artista)
        {
            if (!Id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Artista artistaBack = _artista.GetById(Id.Value);
            if (artistaBack is null)
            { return NotFound(); }
            artistaBack.Nombre = artista.Nombre;
            artistaBack.Pais = artista.Pais;
            artistaBack.Biografia = artista.Biografia;
            _artista.Save(artistaBack);
            return Ok(artistaBack);
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
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
    }
}
