using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models.DTO.Produto
{
    public record ProdutoExibicaoDTO
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public bool Ativo {get; set;}

        public ProdutoExibicaoDTO(Models.Produto p)
        {
            this.Nome = p.Nome;
            this.Descricao = p.Descricao;
            this.Codigo = p.Codigo;
            this.Preco = p.Preco;
            this.QuantidadeEstoque = p.QuantidadeEstoque;
            this.Ativo = p.Ativo;
        }
    }
}