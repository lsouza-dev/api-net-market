using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        [Required]
        public DateTime DataCompra { get; set; }
        public ICollection<CompraProduto> ComprasProdutos {get; set;}

    }
}