using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Application.Dtos.Pais
{
    public class PaisRequestDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string NombrePais { get; set; }
    }
}
