using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models.DTO.CompraProduto;

namespace Market.Models.DTO.Compra
{
    public record CompraExibicaoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string ValorTotal { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Status { get; set; }
        public ICollection<CompraProdutoExibicaoDTO> CompraProdutos { get; set; } = new List<CompraProdutoExibicaoDTO>();

        public CompraExibicaoDTO(Market.Models.Compra c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));

            this.Id = c.Id;
            this.ClienteId = c.Cliente?.Id ?? 0;
            this.Nome = c.Cliente?.Nome ?? string.Empty;
            this.Email = c.Cliente?.Email ?? string.Empty;
            this.ValorTotal = c.ValorTotal.ToString("C2");
            this.DataInicio = c.DataInicio.ToString("dd/MM/yyyy HH:mm:ss");
            this.DataFim = c.DataFim?.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty;
            this.Status = c.Status.ToString();

            // Verifica se ComprasProdutos é null e inicializa com uma lista vazia
            this.CompraProdutos = c.ComprasProdutos != null
                ? c.ComprasProdutos.Select(cp => new CompraProdutoExibicaoDTO(cp)).ToList()
                : new List<CompraProdutoExibicaoDTO>();
        }




    }
}