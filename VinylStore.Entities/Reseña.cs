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
    public class Reseña: IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        [MaxLength(1000)]
        public string Comentario { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaReseña { get; set; }

        public virtual Vinilo Vinilo { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
