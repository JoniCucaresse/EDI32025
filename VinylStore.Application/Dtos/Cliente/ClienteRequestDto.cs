using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Application.Dtos.Cliente
{
    public class ClienteRequestDto
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Nombre { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(500)]
        public string DireccionEnvio { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaDeRegistro { get; set; }
    }
}
