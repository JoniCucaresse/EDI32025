using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Pais : IEntidad
    {
        public Pais()
        {
            PaisesPorArtistas = new HashSet<PaisPorArtista>();
        }
        public int Id { get; set; }
        [StringLength(50)]
        public string NombrePais { get; set; }

        public virtual ICollection<PaisPorArtista> PaisesPorArtistas { get; set; }

    }
}
