using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Market.Models.DTO.Produto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace Market.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Descricao { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        public bool Ativo {get; set;} = true;

        public ICollection<CompraProduto> ComprasProdutos {get; set;}


        public Produto Criar(ProdutoCriacaoDTO dto){
            this.Nome = dto.Nome;
            this.Descricao = dto.Descricao;
            this.Codigo = dto.Codigo;
            this.Preco = dto.Preco;
            this.QuantidadeEstoque = dto.QuantidadeEstoque;
            this.Ativo = true;

            return this;
        }

        public Produto Editar(ProdutoEdicaoDTO dto){
            if(dto.Nome != null) this.Nome = dto.Nome;        
            if(dto.Descricao != null) this.Descricao = dto.Descricao;        
            if(dto.Codigo != null) this.Codigo = dto.Codigo;        
            if(dto.Preco != default(decimal)) this.Preco = dto.Preco;        
            if(dto.QuantidadeEstoque != default(int)) this.QuantidadeEstoque = dto.QuantidadeEstoque;        
            if(dto.Ativo != null) this.Ativo = (bool)dto.Ativo;        

            return this;
        }


        public void Desativar(){
            this.Ativo = false;
        }
    }
}