using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class ArtistaPorVinilo : IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Artista))]
        public int IdArtista { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        public virtual Artista Artista { get; set; }
        public virtual Vinilo Vinilo { get; set; }
    }
}
