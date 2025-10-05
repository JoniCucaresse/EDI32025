using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class GeneroPorVinilo : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Genero))]
        public int IdGenero { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        public virtual Genero Genero { get; set; }
        public virtual Vinilo Vinilo { get; set; }
    }
}
