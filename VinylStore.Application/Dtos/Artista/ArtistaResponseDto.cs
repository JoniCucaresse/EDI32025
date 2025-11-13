using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Application.Dtos.Artista
{
    public class ArtistaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public string Biografia { get; set; }
    }
}
