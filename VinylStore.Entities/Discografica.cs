using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Discografica : IEntidad
    {
        public Discografica()
        {
            Vinilos = new HashSet<Vinilo>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Vinilo> Vinilos { get; set; }
    }
}
