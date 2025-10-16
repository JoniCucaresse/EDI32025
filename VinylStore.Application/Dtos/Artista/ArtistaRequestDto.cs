using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Application.Dtos.Artista
{
    public class ArtistaRequestDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Pais { get; set; }
        public string Biografia { get; set; }
    }
}
