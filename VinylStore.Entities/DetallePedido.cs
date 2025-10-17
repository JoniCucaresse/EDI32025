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
    public class DetallePedido:IEntidad
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Vinilo Vinilo { get; set; }
    }
}
