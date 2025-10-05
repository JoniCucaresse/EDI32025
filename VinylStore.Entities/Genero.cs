using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Genero : IEntidad
    {
        public Genero()
        {
            GenerosPorVinilos = new HashSet<GeneroPorVinilo>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<GeneroPorVinilo> GenerosPorVinilos { get; set; }
    }
}
