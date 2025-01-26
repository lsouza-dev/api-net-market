using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Context;
using Market.Models;
using Market.Models.DTO.Produto;

namespace Market.Services
{
    public class ProdutoService
    {
        private readonly MarketContext _context;

        public ProdutoService(MarketContext context){
            _context = context;
        }

        public Produto Criar(ProdutoCriacaoDTO dto){
            var produto = new Produto().Criar(dto);
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public List<ProdutoExibicaoDTO> Listar()
        {
            var produtos = _context.Produtos.Where(p => p.Ativo == true).ToList();
            var dtos = produtos.Select(p => new ProdutoExibicaoDTO(p)).ToList();
            return dtos;
        }

        public Produto BuscarPorId(int id)
        {
            return _context.Produtos.Where(p => p.Id == id).FirstOrDefault();
        }

        public Produto Editar(int id,ProdutoEdicaoDTO edicaoDTO)
        {
            var produtoDb = BuscarPorId(id);
            produtoDb.Editar(edicaoDTO);
            _context.Produtos.Update(produtoDb);
            _context.SaveChanges();

            return produtoDb;
        }

        public bool Excluir(int id)
        {
            var produtoDb = BuscarPorId(id);
            if(produtoDb == null) return false;

            produtoDb.Desativar();
            _context.Produtos.Update(produtoDb);
            _context.SaveChanges();

            return true;
        }
    }
}