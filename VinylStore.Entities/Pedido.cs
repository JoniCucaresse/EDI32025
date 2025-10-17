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
    public class Pedido:IEntidad
    {
        public Pedido()
        {
            DetallesPedidos= new HashSet<DetallePedido>();
        }
        public int Id { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; set; }
        [MaxLength(50)]
        public string Estado { get; set; } // Pendiente, Procesando, Enviado, Completado, Cancelado
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [MaxLength(100)]
        public string MetodoDePago { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; }
    }
}
