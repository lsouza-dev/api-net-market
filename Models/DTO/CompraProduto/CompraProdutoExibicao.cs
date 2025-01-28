using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Market.Models.DTO.CompraProduto
{
    public record CompraProdutoExibicaoDTO
    {
        public int Id {get; set;}   
        public int CompraId {get; set;}   
        public int ProdutoId {get; set;}
        public int Quantidade {get; set;}
        
        public CompraProdutoExibicaoDTO(Models.CompraProduto cp){
            this.Id = cp.Id;
            this.CompraId = cp.CompraId;
            this.ProdutoId = cp.ProdutoId;
            this.Quantidade = cp.Quantidade;
        }
    }
}