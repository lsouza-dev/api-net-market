using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class CompraProduto
    {
        [Key]
        public int Id { get; set; }

        public int CompraId { get; set; }
        public Compra Compra { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }


        public CompraProduto(){}

        public CompraProduto(Compra compra, Produto produto, int quantidade)
        {
            this.CompraId = compra.Id;
            this.Compra = compra;
            this.ProdutoId = produto.Id;
            this.Produto = produto;
            this.Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"ID: {this.Id} \tCompra ID: {this.CompraId} \tProduto ID: {this.ProdutoId} \tQuantidade: {this.Quantidade}\n";
        
        }
    }
}