using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Cliente:IEntidad
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
            Reseñas = new HashSet<Reseña>();
        }
        public int Id { get; set; }
        [MaxLength(200)]
        public string Nombre { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(500)]
        public string DireccionEnvio { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaDeRegistro { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
