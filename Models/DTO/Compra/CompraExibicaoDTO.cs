using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models.DTO.Compra
{
    public record CompraExibicaoDTO
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string ValorTotal { get; set; } 
        public string DataInicio { get; set; } 
        public string DataFim { get; set; } 
        public string Status { get; set; } 
        public ICollection<CompraProduto> CompraProdutos {get;set;}

        public CompraExibicaoDTO(Market.Models.Compra c){
            this.ClienteId = c.Cliente.Id;
            this.Nome = c.Cliente.Nome;
            this.Email = c.Cliente.Email;
            this.ValorTotal = c.ValorTotal.ToString("C2");
            this.DataInicio = c.DataInicio.ToString("dd/MM/yyyy hh:mm:ss");
            this.DataFim = c.DataFim?.ToString("dd/MM/yyyy hh:mm:ss") ?? string.Empty;
            this.Status = c.Status.ToString();
            this.CompraProdutos = c.ComprasProdutos;
        }
        
    }
}