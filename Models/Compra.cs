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
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal ValorTotal {get; set;}
        [Required]
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusCompra Status {get; set;}
        public ICollection<CompraProduto> ComprasProdutos {get; set;}

        public Compra(){}

        public Compra(Cliente cliente){
            this.ClienteId = cliente.Id;
            this.Cliente = cliente;
            this.ValorTotal = 0.0M;
            this.DataInicio = DateTime.Now;
            this.DataFim = null;
            this.Status = StatusCompra.PENDENTE;
        }

        public bool Finalizar(decimal valor){
            if(valor < this.ValorTotal) return false;
            this.DataFim = DateTime.Now;
            this.Status = StatusCompra.PAGO;
            return true;
        }
    
    }
}