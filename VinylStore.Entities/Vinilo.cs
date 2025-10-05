using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Vinilo : IEntidad
    {
        public Vinilo()
        {
            ArtistasPorVinilos = new HashSet<ArtistaPorVinilo>();
            GenerosPorVinilos = new HashSet<GeneroPorVinilo>();
        }
        public int Id { get; set; }
        [StringLength(50)]
        public string Titulo { get; set; }
        public int? AnioLanzamiento { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        [ForeignKey(nameof(Discografica))]
        public int IdDiscografica { get; set; }
        public virtual Discografica Discografica { get; set; }
        public virtual ICollection<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
        public virtual ICollection<GeneroPorVinilo> GenerosPorVinilos { get; set; }
    }
}
