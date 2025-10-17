using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Application.Dtos.Cliente
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string DireccionEnvio { get; set; }
        public string FechaDeRegistro { get; set; }
    }
}
