using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class PaisPorArtista: IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }
        [ForeignKey(nameof(Artista))]
        public int IdArtista { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Artista Artista { get; set; }
    }
}
