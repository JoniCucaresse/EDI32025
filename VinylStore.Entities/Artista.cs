using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Artista : IEntidad
    {
        public Artista()
        {
            ArtistasPorVinilos = new HashSet<ArtistaPorVinilo>();
        }
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Pais { get; set; }
        public string Biografia { get; set; }
        public virtual ICollection<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
    }
}
